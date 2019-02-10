﻿using Desktop;
using SPNATI_Character_Editor.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;

namespace SPNATI_Character_Editor.EpilogueEditing
{
	public class SpritePreview
	{
		public string Id;
		public PosePreview Pose;
		public Sprite Sprite;
		public Keyframe LinkedFrame;
		public PoseDirective LinkedDirective;
		public KeyframePreview LinkedFramePreview;
		public PoseAnimation LinkedAnimation;
		public Image Image;
		public int AddIndex;
		public float X;
		public float Y;
		public int Width;
		public int Height;
		public int Z;
		public float ScaleX;
		public float ScaleY;
		public float Rotation;
		public float Alpha;
		public float PivotX;
		public float PivotY;
		public bool KeyframeActive;

		public SpritePreview() { }

		public SpritePreview(PosePreview pose, Sprite sprite, int index)
		{
			Sprite = sprite;
			Pose = pose;
			Id = sprite.Id;
			Z = Sprite.Z;
			AddIndex = index;
			int.TryParse(Sprite.Width, out Width);
			int.TryParse(Sprite.Height, out Height);
			if (!string.IsNullOrEmpty(Sprite.Src))
			{
				Pose.Images.TryGetValue(Sprite.Src, out Image);
				if (Width == 0)
				{
					Width = Image.Width;
				}
				if (Height == 0)
				{
					Height = Image.Height;
				}
			}
			PivotX = SceneObject.ParsePivot(Sprite.PivotX ?? "center", Width);
			PivotY = SceneObject.ParsePivot(Sprite.PivotY ?? "center", Height);
			SetInitialParameters();
		}

		public void SetInitialParameters()
		{	
			float.TryParse(Sprite.X, NumberStyles.Number, CultureInfo.InvariantCulture, out X);
			float.TryParse(Sprite.Y, NumberStyles.Number, CultureInfo.InvariantCulture, out Y);
			if (!float.TryParse(Sprite.ScaleX, NumberStyles.Number, CultureInfo.InvariantCulture, out ScaleX))
			{
				ScaleX = 1;
			}
			if (!float.TryParse(Sprite.ScaleY, NumberStyles.Number, CultureInfo.InvariantCulture, out ScaleY))
			{
				ScaleY = 1;
			}
			float.TryParse(Sprite.Rotation, NumberStyles.Number, CultureInfo.InvariantCulture, out Rotation);
			float.TryParse(Sprite.Opacity ?? "100", NumberStyles.Number, CultureInfo.InvariantCulture, out Alpha);
		}

		public override string ToString()
		{
			return Id;
		}

		public void Draw(Graphics g, int displayWidth, int displayHeight, Point offset)
		{
			float alpha = Alpha;
			if (KeyframeActive)
			{
				alpha *= 0.5f;
			}
			if (Image != null && alpha > 0)
			{
				Rectangle bounds = ToScreenRegion(displayWidth, displayHeight, offset);	

				float offsetX = bounds.X + PivotX / Width * bounds.Width;
				float offsetY = bounds.Y + PivotY / Height * bounds.Height;
				if (float.IsNaN(offsetX))
				{
					offsetX = 0;
				}
				if (float.IsNaN(offsetY))
				{
					offsetY = 0;
				}

				g.TranslateTransform(offsetX, offsetY);
				g.RotateTransform(Rotation);
				g.TranslateTransform(-offsetX, -offsetY);

				if (alpha < 100)
				{
					float[][] matrixItems = new float[][] {
							new float[] { 1, 0, 0, 0, 0 },
							new float[] { 0, 1, 0, 0, 0 },
							new float[] { 0, 0, 1, 0, 0 },
							new float[] { 0, 0, 0, alpha / 100.0f, 0 },
							new float[] { 0, 0, 0, 0, 1 }
						};
					ColorMatrix cm = new ColorMatrix(matrixItems);
					ImageAttributes ia = new ImageAttributes();
					ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					g.DrawImage(Image, bounds, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, ia);
				}
				else
				{
					g.DrawImage(Image, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height), new Rectangle(0, 0, Image.Width, Image.Height), GraphicsUnit.Pixel);
				}

				g.ResetTransform();
			}
		}

		/// <summary>
		/// Converts an object's bounds to screen space, ensuring that the width and height are positive
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public RectangleF ToAbsScreenRegion(int displayWidth, int displayHeight, Point offset)
		{
			RectangleF region = ToScreenRegion(displayWidth, displayHeight, offset);
			if (region.Width < 0)
			{
				region.X += region.Width;
				region.Width = -region.Width;
			}
			if (region.Height < 0)
			{
				region.Y += region.Height;
				region.Height = -region.Height;
			}
			return region;
		}

		/// <summary>
		/// Converts an object's unscaled bounds to screen space
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public RectangleF ToUnscaledScreenRegion(int displayWidth, int displayHeight, Point offset)
		{
			float x = ScaleToDisplay(X, displayHeight);
			float y = ScaleToDisplay(Y, displayHeight);
			float width = ScaleToDisplay(Width, displayHeight);
			float height = ScaleToDisplay(Height, displayHeight);
			x = (int)(x + displayWidth * 0.5f - width * 0.5f);
			return new RectangleF(offset.X + x, offset.Y + y, width, height);
		}

		public Rectangle ToScreenRegion(int displayWidth, int displayHeight, Point offset)
		{
			//get unscaled bounds in screen space
			float x = ScaleToDisplay(X, displayHeight);
			float y = ScaleToDisplay(Y, displayHeight);
			float width = ScaleToDisplay(Width, displayHeight);
			float height = ScaleToDisplay(Height, displayHeight);
			x = (int)(x + displayWidth * 0.5f - width * 0.5f);

			//translate pivot to origin
			float pivotX = x + PivotX / Width * width;
			float pivotY = y + PivotY / Height * height;
			x -= pivotX;
			y -= pivotY;

			//apply scaling
			float right = x + width;
			x *= ScaleX;
			right *= ScaleX;
			width = right - x;

			float bottom = y + height;
			y *= ScaleY;
			bottom *= ScaleY;
			height = bottom - y;

			//translate back
			x += pivotX;
			y += pivotY;

			return new Rectangle(offset.X + (int)x, offset.Y + (int)y, (int)width, (int)height);
		}

		/// <summary>
		/// Gets the "center" of an object's selection
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public Point ToScreenCenter(int displayWidth, int displayHeight, Point offset)
		{
			RectangleF bounds = ToScreenRegion(displayWidth, displayHeight, offset);
			float cx = bounds.X + bounds.Width / 2;
			float cy = bounds.Y + bounds.Height / 2;
			return new Point((int)cx, (int)cy);
		}

		public int ScaleToDisplay(float value, int canvasHeight)
		{
			return (int)Math.Floor(value * canvasHeight / Pose.BaseHeight);
		}

		/// <summary>
		/// Updates the sprite's position to a new value, updating the underlying data structures too
		/// </summary>
		/// <returns>List of objects that were modified</returns>
		public IEnumerable<object> AdjustPosition(int x, int y)
		{
			if (X == x && Y == y)
			{
				yield break;
			}
			X = x;
			Y = y;

			foreach (object o in AdjustProperty("X", x, x.ToString()))
			{
				yield return o;
			}
			foreach (object o in AdjustProperty("Y", y, y.ToString()))
			{
				yield return o;
			}
		}

		public IEnumerable<object> AdjustScale(Point screenPoint, int displayWidth, int displayHeight, Point offset, Point startPoint, HoverContext context, bool locked)
		{
			bool horizontal = (context & HoverContext.ScaleHorizontal) != 0;
			bool vertical = (context & HoverContext.ScaleVertical) != 0;

			RectangleF bounds = ToUnscaledScreenRegion(displayWidth, displayHeight, offset);
			PointF targetPoint = new PointF(screenPoint.X, screenPoint.Y);
			PointF sourcePoint = new PointF(bounds.X, bounds.Y); //unscaled point corresponding to the point being dragged
			if (context.HasFlag(HoverContext.ScaleRight))
			{
				sourcePoint.X += bounds.Width;
			}
			if (context.HasFlag(HoverContext.ScaleBottom))
			{
				sourcePoint.Y += bounds.Height;
			}

			PointF pivot = new PointF(bounds.X + PivotX / Width * bounds.Width, bounds.Y + PivotY / Height * bounds.Height);
			//shift pivot to origin

			sourcePoint.X -= pivot.X;
			sourcePoint.Y -= pivot.Y;

			targetPoint.X -= pivot.X;
			targetPoint.Y -= pivot.Y;

			//determine scalar to get reach given point
			float mx = targetPoint.X / sourcePoint.X;
			float my = targetPoint.Y / sourcePoint.Y;

			if (float.IsInfinity(mx))
			{
				mx = 0.001f;
			}
			if (float.IsInfinity(my))
			{
				my = 0.001f;
			}

			if (ScaleX != mx && horizontal)
			{
				ScaleX = mx;
				foreach (object o in AdjustProperty("ScaleX", mx, mx.ToString(CultureInfo.InvariantCulture)))
				{
					yield return o;
				}
			}
			if (ScaleY != my && vertical)
			{
				ScaleY = my;
				foreach (object o in AdjustProperty("ScaleY", my, my.ToString(CultureInfo.InvariantCulture)))
				{
					yield return o;
				}
			}
		}

		public IEnumerable<object> AdjustRotation(Point screenPoint, Point screenCenter)
		{
			//quick and dirty - just use the angle to look from the point to the center

			double angle = Math.Atan2(screenCenter.Y - screenPoint.Y, screenCenter.X - screenPoint.X);
			angle = angle * (180 / Math.PI) - 90;

			if (Rotation == angle)
			{
				yield break;
			}

			Rotation = (float)angle;
			foreach (object o in AdjustProperty("Rotation", (float)angle, angle.ToString(CultureInfo.InvariantCulture)))
			{
				yield return o;
			}
		}

		private static DualKeyDictionary<Type, string, FieldInfo> _fields = new DualKeyDictionary<Type, string, FieldInfo>();
		private static FieldInfo GetField<T>(string name)
		{
			FieldInfo fi = _fields.Get(typeof(T), name);
			if (fi == null)
			{
				fi = typeof(T).GetField(name);
				_fields.Set(typeof(T), name, fi);
			}
			return fi;
		}

		private static string GetValue<T>(T obj, string name)
		{
			FieldInfo fi = GetField<T>(name);
			return fi.GetValue(obj)?.ToString();
		}

		private static void SetValue<T>(T obj, string name, object value)
		{
			FieldInfo fi = GetField<T>(name);
			fi.SetValue(obj, value);
		}

		private IEnumerable<object> AdjustProperty(string property, object value, string strValue)
		{
			if (LinkedFrame == null)
			{
				SetValue(Sprite, property, strValue);
				yield return Sprite;

				//when updating the root sprite, also update the first keyframe that touches these properties
				for (int i = 0; i < Pose.Pose.Directives.Count; i++)
				{
					PoseDirective dir = Pose.Pose.Directives[i];
					if (dir.DirectiveType == "animation" && dir.Id == Sprite.Id && dir.Keyframes.Count > 0)
					{
						Keyframe frame = dir.Keyframes[0];
						string frameValue = GetValue(frame, property);
						if (!string.IsNullOrEmpty(frameValue))
						{
							SetValue(frame, property, strValue);
							yield return frame;
							break;
						}
					}
				}
			}
			else
			{
				SetValue(LinkedFrame, property, strValue);
				yield return LinkedFrame;

				//when updating the first keyframe of a directive, also update the sprite or the last frame of the previous animation affecting this
				int frameIndex = LinkedDirective.Keyframes.IndexOf(LinkedFrame);
				if (frameIndex == 0)
				{
					bool updated = false;
					int index = Pose.Pose.Directives.IndexOf(LinkedDirective);
					for (int i = index - 1; i >= 0; i--)
					{
						PoseDirective dir = Pose.Pose.Directives[i];
						if (dir.DirectiveType == "animation" && dir.Id == Sprite.Id && dir.Keyframes.Count > 0)
						{
							Keyframe frame = dir.Keyframes[dir.Keyframes.Count - 1];
							string frameValue = GetValue(frame, property);
							if (!string.IsNullOrEmpty(frameValue))
							{
								SetValue(frame, property, strValue);
								yield return frame;
								updated = true;
								break;
							}
						}
					}
					if (!updated && !string.IsNullOrEmpty(GetValue(Sprite, property)))
					{
						SetValue(Sprite, property, strValue);
						yield return Sprite;
					}
				}
			}

			if (LinkedFramePreview != null)
			{
				SetValue(LinkedFramePreview, property, value);
			}
		}

		public bool AdjustPivot(Point screenPt, RectangleF worldBounds)
		{
			float xPct = (screenPt.X - worldBounds.X) / worldBounds.Width;
			float yPct = (screenPt.Y - worldBounds.Y) / worldBounds.Height;
			float pivotX = xPct * Width;
			float pivotY = yPct * Height;
			if (pivotX == PivotX && pivotY == PivotY)
			{
				return false;
			}
			PivotX = pivotX;
			PivotY = pivotY;
			Sprite.PivotX = ((int)(xPct * 100)).ToString() + "%";
			Sprite.PivotY = ((int)(yPct * 100)).ToString() + "%";
			return true;
		}
	}
}
