﻿using Desktop;
using KisekaeImporter;
using KisekaeImporter.ImageImport;
using SPNATI_Character_Editor.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Activities
{
	[Spacer]
	[Activity(typeof(Character), 200)]
	public partial class PoseListEditor : Activity
	{
		private const int MiniHeight = 200;
		private ImageLibrary _imageLibrary;
		private ImageMetadata _previewImageMetadata;
		private Character _character;
		private PoseList _poseList = new PoseList();
		private string _lastPoseFile;

		private readonly Image EmptyImage = new Bitmap(1, 1);

		public override string Caption
		{
			get { return "Poses"; }
		}

		public PoseListEditor()
		{
			InitializeComponent();

			ColImage.DefaultCellStyle.NullValue = null;
		}

		protected override void OnInitialize()
		{
			_character = Record as Character;	
		}

		protected override void OnFirstActivate()
		{
			_imageLibrary = ImageLibrary.Get(_character);
			string defaultFileName = Path.Combine(Config.GetRootDirectory(_character), "poses.txt");
			if (File.Exists(defaultFileName))
			{
				ImportPoseList(defaultFileName);
			}
			PopulatePoseGrid();
		}

		public override void Quit()
		{
			CleanUpGrid();
		}

		protected override void OnParametersUpdated(params object[] parameters)
		{
			if (parameters.Length >= 1)
			{
				PoseList list = parameters[0] as PoseList;
				_poseList = list;
				PopulatePoseGrid();
				lblCurrentPoseFile.Text = "From template";
			}
		}

		/// <summary>
		/// Clears all poses in the grid
		/// </summary>
		private void ClearPoses()
		{
			CleanUpGrid();
			_poseList.Poses.Clear();
		}

		/// <summary>
		/// Imports a pose list from a file
		/// </summary>
		/// <param name="filename"></param>
		private void ImportPoseList(string filename)
		{
			_poseList = new PoseList();
			string[] lines = File.ReadAllLines(filename);

			Rect cropInfo = new Rect(0, 0, 600, 1400);

			foreach (string line in lines)
			{
				string trimmedLine = line.Trim();
				if (trimmedLine.StartsWith("#") || string.IsNullOrEmpty(trimmedLine))
					continue;
				string[] pieces = trimmedLine.Split('=');

				if (pieces.Length < 2)
					continue;

				if (pieces[0] == "crop_pixels")
				{
					//Update global crop info
					string[] pixels = pieces[1].Split(',');
					if (pixels.Length != 4)
						continue;
					int l = 0;
					int t = 0;
					int r = 0;
					int b = 0;
					if (!int.TryParse(pixels[0], out l) || !int.TryParse(pixels[1], out t) || !int.TryParse(pixels[2], out r) || !int.TryParse(pixels[3], out b))
						continue;
					cropInfo = new Rect(l, t, r, b);
					continue;
				}

				string key = pieces[0];
				//TODO: check for crop_images
				string data = pieces[1];

				ImageMetadata metadata = new ImageMetadata(key, data);
				metadata.CropInfo = cropInfo;
				_poseList.Poses.Add(metadata);
			}

			lblCurrentPoseFile.Text = Path.GetFileName(filename);
		}

		/// <summary>
		/// Exports the pose grid to a file
		/// </summary>
		/// <param name="filename">Full file path of the file to create/replace</param>
		private void ExportPoseList(string filename)
		{
			//First create the pose list from the grid
			MakePoseList();
			List<string> lines = new List<string>();

			Rect currentCrop = new Rect(0, 0, 600, 1400);
			foreach (ImageMetadata metadata in _poseList.Poses)
			{
				Rect crop = metadata.CropInfo;
				if (currentCrop != crop)
				{
					//If new crop values are found, put them in
					lines.Add(string.Format("crop_pixels={0}", crop.Serialize()));
					currentCrop = crop;
				}
				lines.Add(metadata.Serialize());
			}
			File.WriteAllLines(filename, lines);
			lblCurrentPoseFile.Text = Path.GetFileName(filename);
		}

		/// <summary>
		/// Builds the PoseList data structure from the info in the grid
		/// </summary>
		private void MakePoseList()
		{
			_poseList = new PoseList();
			foreach (DataGridViewRow row in gridPoses.Rows)
			{
				string stage = row.Cells["ColStage"].Value?.ToString();
				string pose = row.Cells["ColPose"].Value?.ToString();
				if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
					continue;
				int l = 0;
				int r = 0;
				int t = 0;
				int b = 0;
				int.TryParse(row.Cells["ColL"].Value?.ToString(), out l);
				if (!int.TryParse(row.Cells["ColR"].Value?.ToString(), out r))
					r = 600;
				int.TryParse(row.Cells["ColT"].Value?.ToString(), out t);
				if (!int.TryParse(row.Cells["ColB"].Value?.ToString(), out b))
					b = 1400;

				string data = row.Cells["ColData"].Value?.ToString();
				if (data == null)
					data = string.Empty;

				string key = GetKey(stage, pose);
				ImageMetadata metadata = new ImageMetadata(key, data);
				metadata.CropInfo = new Rect(l, t, r, b);
				_poseList.Poses.Add(metadata);
			}
		}

		/// <summary>
		/// Builds a filename from a stage and pose/emotion
		/// </summary>
		/// <param name="stage"></param>
		/// <param name="pose"></param>
		/// <returns></returns>
		private static string GetKey(string stage, string pose)
		{
			return string.Format("{0}-{1}", stage, pose);
		}

		private void CleanUpGrid()
		{
			foreach (DataGridViewRow row in gridPoses.Rows)
			{
				CharacterImage image = row.Tag as CharacterImage;
				if (image != null)
				{
					image.ReleaseImage();
				}
			}
			gridPoses.Rows.Clear();
		}

		/// <summary>
		/// Populates the pose grid
		/// </summary>
		private void PopulatePoseGrid()
		{
			DataGridViewImageColumn imageCol = gridPoses.Columns["ColImage"] as DataGridViewImageColumn;
			imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

			const int InitialImageThreshold = 20; //number of images to initially display before incrementally loading the rest
			int imageIndex = 0;

			try
			{
				CleanUpGrid();
				foreach (ImageMetadata pose in _poseList.Poses)
				{
					DataGridViewRow row = gridPoses.Rows[gridPoses.Rows.Add()];
					string[] piecedKey = pose.ImageKey.Split(new char[] { '-' }, 2);
					if (piecedKey.Length == 0)
					{
						row.Cells["ColStage"].Value = "0";
						row.Cells["ColPose"].Value = pose.ImageKey;
					}
					else
					{
						row.Cells["ColStage"].Value = piecedKey[0];
						row.Cells["ColPose"].Value = piecedKey[1];
					}

					row.Cells["ColL"].Value = pose.CropInfo.Left;
					row.Cells["ColR"].Value = pose.CropInfo.Right;
					row.Cells["ColT"].Value = pose.CropInfo.Top;
					row.Cells["ColB"].Value = pose.CropInfo.Bottom;

					row.Cells["ColData"].Value = pose.Data;
					if (imageIndex < InitialImageThreshold)
					{
						imageIndex++;
						UpdateImageCell(pose.ImageKey, row);
					}
				}
			}
			catch
			{
				MessageBox.Show("Error importing pose list. Is this actually a poses file?", "Import Pose List", MessageBoxButtons.OK, MessageBoxIcon.Error);
				gridPoses.Rows.Clear();
			}
		}

		private void UpdateImageCell(string key, DataGridViewRow row)
		{
			CharacterImage cached = row.Tag as CharacterImage;
			cached?.ReleaseImage();
			CharacterImage existingImage = _imageLibrary.GetMini(key, MiniHeight);
			DataGridViewImageCell imageCell = row.Cells["ColImage"] as DataGridViewImageCell;
			if (existingImage != null)
			{
				imageCell.Value = existingImage.GetImage();
				row.Tag = existingImage;
				row.Cells["ColImport"].Value = "Reimport";
			}
			else
			{
				imageCell.Value = EmptyImage;
				row.Cells["ColImport"].Value = "Import";
			}
		}

		/// <summary>
		/// Handles the Import buttons within the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (gridPoses.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
			{
				ImportImageForCropping(e.RowIndex);
			}
		}

		/// <summary>
		/// Imports an image from Kisekae and brings up the cropper
		/// </summary>
		/// <param name="index">Pose grid row index containing the metadata of the pose to import</param>
		private void ImportImageForCropping(int index)
		{
			DataGridViewRow row = gridPoses.Rows[index];

			string stage = row.Cells["ColStage"].Value?.ToString();
			string pose = row.Cells["ColPose"].Value?.ToString();
			if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
			{
				MessageBox.Show("Stage and Pose must be filled out.", "Import Pose", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string data = row.Cells["ColData"].Value?.ToString();
			if (string.IsNullOrEmpty(data))
			{
				//Dummy values for faster testing
				data = "47**aa17.100.0.14.62.17.100.0.2.62_ab_ac_ba54_bb7.1_bc185.500.0.0.1_bd17_be180_ca79.0.6.63.50.31.44.49.30_cb0_daF9C9BD.0.0.100_db_dd9.3.16.0.12.29_dhF38484.20.50.43.3_di5_qa_qb_dc7.4.EEBABE.D68B8B.C96262_eh_ea26.33.33.56.0.0_ec10.29.33.33.56.42.63.1_ed0.24.0.1.33.56_ef_eg_r00_fa11.50.65.54.50.36.56_fb7_fh5_fc3.19.55.3.19.55.42.61.61.42.50.50_fd1.0.20.893A1A.893A1A_fe48.77_ff0000000000_fg1.58_pa0.0.0.0.10.50.85.78.0.0_t00_pb_pc_pd_pe_ga0_gb0_gc1.0_ge0000000000_gh_gf_gg_gd10000000_ha86.86_hb50.1.50.100_hc0.50.48.0.50.48_hd0.1.50.50_ia_if0.55.55.0.1.8.0.0.8.0.0.0.0.3_ib_id9.55.55.44.0.0.1.1.0.0.1.0.0.3_ic_jc_ie1.56.56.0.10.22.22.0.10.22.22.0.0_ja13.55.2.0_jb13.55.2.0_jd6.48.50.50_je6.48.50.50_jf_jg_ka4.18.18.18.0_kb10.A5C0F1.42.42_kc_kd_ke_kf_la_lb_oa_os_ob_oc_od_oe_of_lc_m00_n00_s00_og_oh_oo_op_oq_or_om_on_ok_ol_oi_oj_ad0.0.0.0.0.0.0.0.0.0";
			}
			string key = GetKey(stage, pose);

			_previewImageMetadata = new ImageMetadata(key, data);
			int l = 0;
			int r = 0;
			int t = 0;
			int b = 0;
			int.TryParse(row.Cells["ColL"].Value?.ToString(), out l);
			if (!int.TryParse(row.Cells["ColR"].Value?.ToString(), out r))
				r = 600;
			int.TryParse(row.Cells["ColT"].Value?.ToString(), out t);
			if (!int.TryParse(row.Cells["ColB"].Value?.ToString(), out b))
				b = 1400;
			_previewImageMetadata.CropInfo = new Rect(l, t, r, b);

			ImageCropper cropper = new ImageCropper();
			cropper.Import(_previewImageMetadata, _character, false);
			if (cropper.ShowDialog() == DialogResult.OK)
			{
				Image importedImage = cropper.CroppedImage;
				if (importedImage != null)
				{
					SaveImage(key, importedImage);
					UpdateImageCell(key, row);
					Rect crop = cropper.CroppingRegion;
					row.Cells["ColL"].Value = crop.Left;
					row.Cells["ColR"].Value = crop.Right;
					row.Cells["ColT"].Value = crop.Top;
					row.Cells["ColB"].Value = crop.Bottom;
				}			
			}
		}

		/// <summary>
		/// Clears out red Xs from rows that have no image yet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			DataGridViewRow row = gridPoses.Rows[e.RowIndex];
			if (row.IsNewRow)
			{
				row.Cells["ColImage"].Value = EmptyImage;
			}
		}

		/// <summary>
		/// Defaults values for new poses in the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridPoses_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			for (int i = 0; i < e.RowCount; i++)
			{
				int index = e.RowIndex + i;
				DataGridViewRow row = gridPoses.Rows[index];
				row.Cells["ColImport"].Value = "Import";
				row.Cells["ColL"].Value = "0";
				row.Cells["ColT"].Value = "0";
				row.Cells["ColR"].Value = "600";
				row.Cells["ColB"].Value = "1400";
			}
		}

		/// <summary>
		/// Wrapper around a file dialog to force the file selection to be within the character's folder
		/// </summary>
		/// <param name="dialog">Dialog to open</param>
		/// <returns></returns>
		private DialogResult ChooseFileInDirectory(FileDialog dialog, ref string file)
		{
			string dir = Config.GetRootDirectory(_character);
			dialog.InitialDirectory = dir;
			dialog.FileName = Path.GetFileName(file);
			DialogResult result = DialogResult.OK;
			bool invalid;
			do
			{
				invalid = false;
				result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					if (Path.GetDirectoryName(dialog.FileName) != dir)
					{
						MessageBox.Show("Images need to be in the character's folder.", "Import Images", MessageBoxButtons.OK, MessageBoxIcon.Error);
						invalid = true;
					}
				}
			}
			while (invalid);
			if (result == DialogResult.OK)
				file = dialog.FileName;
			return result;
		}

		/// <summary>
		/// Click event for the Load Pose List button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdImport_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(openFileDialog1, ref _lastPoseFile) == DialogResult.OK)
			{
				ImportPoseList(openFileDialog1.FileName);
				PopulatePoseGrid();
			}
		}

		/// <summary>
		/// Click event for the Save Pose List button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdExport_Click(object sender, EventArgs e)
		{
			if (ChooseFileInDirectory(saveFileDialog1, ref _lastPoseFile) == DialogResult.OK)
			{
				ExportPoseList(saveFileDialog1.FileName);
			}
		}

		/// <summary>
		/// Click event for the Clear Poses button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This will clear all poses and lose any unsaved changes. Are you sure you want to continue?", "Clear Pose List", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				ClearPoses();
			}
		}

		/// <summary>
		/// Click event for the Import New button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdImportNew_Click(object sender, EventArgs e)
		{
			ImportUnloadedPoses();
		}

		/// <summary>
		/// Click event for the Import All button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdImportAll_Click(object sender, EventArgs e)
		{
			ImportAllPoses();
		}

		/// <summary>
		/// Imports all pose data that doesn't have an image yet
		/// </summary>
		private void ImportUnloadedPoses()
		{
			MakePoseList();

			//Figure out which images need importing
			List<ImageMetadata> toImport = new List<ImageMetadata>();
			foreach (var metadata in _poseList.Poses)
			{
				CharacterImage existingImage = _imageLibrary.Find(metadata.ImageKey);
				if (existingImage != null)
					continue;
				toImport.Add(metadata);
			}

			ImportPosesAsync(toImport);
		}

		/// <summary>
		/// Imports all poses, replacing existing images
		/// </summary>
		private void ImportAllPoses()
		{
			MakePoseList();
			ImportPosesAsync(_poseList.Poses);
		}

		/// <summary>
		/// Imports images asynchronously with a progress form
		/// </summary>
		/// <param name="list"></param>
		private void ImportPosesAsync(List<ImageMetadata> list)
		{
			ProgressForm progressForm = new ProgressForm();
			progressForm.Text = "Import New Poses";
			progressForm.Show(this);

			int count = list.Count;
			var progressUpdate = new Progress<int>(value => progressForm.SetProgress(string.Format("Importing {0}...", list[value].ImageKey), value, count));

			progressForm.Shown += async (s, args) =>
			{
				var cts = new CancellationTokenSource();
				progressForm.SetCancellationSource(cts);
				try
				{
					int result = await ImportPoses(progressUpdate, list, cts.Token);
					if (result < 0)
					{
						MessageBox.Show("Imported with errors. See errorlog.txt for more information.", "Import Poses", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				finally
				{
					progressForm.Close();
				}
			};
		}

		/// <summary>
		/// Imports the provided pose data into images
		/// </summary>
		/// <param name="progress"></param>
		/// <param name="importList"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		private Task<int> ImportPoses(IProgress<int> progress, List<ImageMetadata> importList, CancellationToken ct)
		{
			return Task.Run(async () =>
			{
				try
				{
					int current = 0;
					bool hasErrors = false;
					List<string> filesToCompress = new List<string>();
					foreach (ImageMetadata metadata in importList)
					{
						progress.Report(current++);
						try
						{
							Image img = await CharacterGenerator.GetCroppedImage(new KisekaeCode(metadata.Data), metadata.CropInfo, _character);
							if (img == null)
							{
								//Something went wrong. Stop here.
								MessageBox.Show("Couldn't import " + metadata.ImageKey + ". Is Kisekae running?", "Import Pose", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return -1;
							}

							string savePath = SaveImage(metadata.ImageKey, img);

							//Update the relevant grid row
							MethodInvoker invoker = delegate ()
							{
								foreach (DataGridViewRow row in gridPoses.Rows)
								{
									string stage = row.Cells["ColStage"].Value?.ToString();
									string pose = row.Cells["ColPose"].Value?.ToString();
									if (string.IsNullOrEmpty(stage) || string.IsNullOrEmpty(pose))
										continue;
									if (GetKey(stage, pose) == metadata.ImageKey)
									{
										UpdateImageCell(metadata.ImageKey, row);
										row.Cells["ColImport"].Value = "Reimport";
										break;
									}
								}
							};
							gridPoses.Invoke(invoker);
						}
						catch (Exception e)
						{
							hasErrors = true;
							ErrorLog.LogError(string.Format("Error importing from kisekae: {0}, {1}", metadata.ImageKey, e.Message));
						}

						ct.ThrowIfCancellationRequested();
					}

					return hasErrors ? -1 : 1;
				}
				catch (OperationCanceledException)
				{
					return 0;
				}
			}, ct);
		}

		/// <summary>
		/// Saves an image to disk
		/// </summary>
		/// <param name="imageKey">Name of image (stage+pose)</param>
		/// <param name="image">Image to save</param>
		private string SaveImage(string imageKey, Image image)
		{
			string filename = imageKey + ".png";
			string fullPath = Path.Combine(Config.GetRootDirectory(_character), filename);

			//Back up the existing image if it hasn't been backed up yet
			if (File.Exists(fullPath))
			{
				string backupDir = Path.Combine(Config.AppDataDirectory, _character.FolderName);
				if (!Directory.Exists(backupDir))
				{
					Directory.CreateDirectory(backupDir);
				}
				string backupPath = Path.Combine(backupDir, imageKey + ".png");
				if (!File.Exists(backupPath))
				{
					File.Copy(fullPath, backupPath);
				}
			}

			image.Save(fullPath);

			_imageLibrary.Replace(fullPath, image);
			return fullPath;
		}

		private void gridPoses_Scroll(object sender, ScrollEventArgs e)
		{
			DoIncrementalLoad();
		}

		/// <summary>
		/// Loads in poses for all rows currently visible
		/// </summary>
		private void DoIncrementalLoad()
		{
			bool startedDisplay = false;
			foreach (DataGridViewRow row in gridPoses.Rows)
			{
				if (row.Displayed || startedDisplay)
				{
					Image current = row.Cells["ColImage"].Value as Image;
					if (current != null)
					{
						continue;
					}

					startedDisplay = true;
					string stage = row.Cells["ColStage"].Value?.ToString();
					string poseKey = row.Cells["ColPose"].Value?.ToString();
					if (string.IsNullOrEmpty(poseKey) || string.IsNullOrEmpty(stage)) { continue; }

					string imageKey = $"{stage}-{poseKey}";
					//Try to link this up with an existing image. Use thumbnails to save on memory since this activity could load hundreds at once
					UpdateImageCell(imageKey, row);

					if (!row.Displayed) { return; } //Displayed is false for a row that is partially visible, so we do one extra iteration to load it
				}
			}
		}
	}
}