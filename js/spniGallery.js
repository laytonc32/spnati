/********************************************************************************
 This file contains the variables and functions that form the gallery screens of
 the game.
 ********************************************************************************/

 /**********************************************************************
  *****                   Gallery Screen UI Elements               *****
  **********************************************************************/

$galleryEndingsScreen = $('#epilogue-gallery-screen');
$galleryCollectiblesScreen = $('#collectible-gallery-screen');

/**********************************************************************
 *****          Epilogues Gallery Screen UI Elements              *****
 **********************************************************************/
 
$genderTypeButtons = [$('#gallery-gender-all'), $('#gallery-gender-any'), $('#gallery-gender-male'), $('#gallery-gender-female')];
$galleryEndings = $('#gallery-endings-block').children();
$galleryPrevButton = $('#gallery-prev-page-button');
$galleryNextButton = $('#gallery-next-page-button');
$galleryStartButton = $('#gallery-start-ending-button');
$selectedEndingPreview = $('#selected-ending-previev');
$selectedEndingLabels = [$('#selected-ending-title'), $('#selected-ending-character'), $('#selected-ending-gender')];
$selectedEndingHint = [$('#selected-ending-hint-container'), $('#selected-ending-hint')];

/**********************************************************************
 *****          Collectibles Gallery Screen UI Elements           *****
 **********************************************************************/
$collectibleListPane = $('#collectibles-list-pane');
$collectibleImagePane = $('#collectibles-image-pane');
$collectibleTextPane = $('#collectibles-text-pane');
 
$collectibleTitle = $('#collectible-title');
$collectibleSubtitle = $('#collectible-subtitle');
$collectibleCharacter = $('#collectible-character');
$collectibleUnlock = $('#collectible-unlock');
$collectibleText = $('#collectible-text');
$collectibleImage = $('#collectible-image');

function GEnding(player, ending){
	this.player = player;
	this.gender = $(ending).attr('gender');

	var previewImage = $(ending).attr('img');
	if (previewImage) {
		previewImage = previewImage.charAt(0) === '/' ? previewImage : player.base_folder + previewImage;
	} else {
		console.log("No preview image found for: "+player.id+" ending: "+$(ending).html());
	}

	this.image = previewImage;
	this.title = $(ending).html();
	this.unlockHint = $(ending).attr('hint');
	this.unlocked = function() { return EPILOGUES_UNLOCKED || save.hasEnding(player.id, this.title); };
}

var unescapeSubstitutions = {
    '&lt;': '<',
    '&gt;': '>',
    '&quot;': '"',
    '&apos;': '\'',
    '&amp;': '&',
}
var unescapeDialogueRE = /(&(?:lt|gt|quot|apos|amp);)/gi

function unescapeHTML(in_text) {
    return in_text.replace(unescapeDialogueRE, function (match, p1) {
        return unescapeSubstitutions[p1];
    });
}

/* Base class for all collectibles. */
function Collectible(id, image, thumbnail, source, title, subtitle, unlock_hint, text) {
	this.id = id;
	this.image = image;
	this.thumbnail = thumbnail;
	this.source = source;
	this.title = title;
	this.subtitle = subtitle;
	this.unlock_hint = unlock_hint;
	this.text = text;
}

Collectible.prototype.display = function () {
	$collectibleTitle.html(unescapeHTML(this.title));
	$collectibleSubtitle.html(unescapeHTML(this.subtitle));
	$collectibleCharacter.text(this.source);
	$collectibleUnlock.html(unescapeHTML(this.unlock_hint));
	$collectibleText.html(unescapeHTML(this.text));
	
	if (this.image) {
		$collectibleImage.attr('src', this.image);
		$collectibleImagePane.show();
	} else {
		$collectibleImagePane.hide();
	}
};

Collectible.prototype.listElement = function () {
	var baseElem = $('<div class="collectibles-list-item bordered"></div>');
	var imgElem = $('<img class="collectibles-item-icon">').attr('src', this.thumbnail);
	var titleElem = $('<div class="collectibles-item-title"></div>');
	var subtitleElem = $('<div class="collectibles-item-subtitle"></div>');
	
	titleElem.html(this.title);
	subtitleElem.html(this.subtitle);
	
	baseElem.append(imgElem, titleElem, subtitleElem).click(this.display.bind(this));
	
	return baseElem;
};


/* Class for collectibles attached to a specific player.*/
function PlayerCollectible(player, xmlElem) {
	this.player = player;
	this.id = xmlElem.attr('id');
	this.source = player.label;
	this.image = player.folder + xmlElem.attr('img');
	this.thumbnail = xmlElem.attr('thumbnail');
	this.title = xmlElem.find('title').text();
	this.subtitle = xmlElem.find('subtitle').text();	
	this.unlock_hint = xmlElem.find('unlock').text();
	this.text = xmlElem.find('text').html();
}
PlayerCollectible.prototype = Object.create(Collectible.prototype);
PlayerCollectible.prototype.constructor = PlayerCollectible;


/* Class for collectibles not attached to a specific character.
 * These are marked as coming from The Inventory itself. */
function InventoryCollectible(xmlElem) {
	this.id = xmlElem.attr('id');
	this.source = 'The Inventory';
	this.image = xmlElem.attr('img');
	this.thumbnail = xmlElem.attr('thumbnail');
	this.title = xmlElem.find('title').text();
	this.subtitle = xmlElem.find('subtitle').text();	
	this.unlock_hint = xmlElem.find('unlock').text();
	this.text = xmlElem.find('text').html();
}
InventoryCollectible.prototype = Object.create(Collectible.prototype);
InventoryCollectible.prototype.constructor = InventoryCollectible;


/**********************************************************************
 *****                   Gallery Screen UI Functions              *****
 **********************************************************************/
 
 
/* opponent listing file */
var galleryEndings = [];
var allEndings = [];
var anyEndings = [];
var maleEndings = [];
var femaleEndings = [];
var galleryPage = 0;
var galleryPages = -1;
var epp = 20;
var selectedEnding = -1;
var GALLERY_GENDER = 'all';

var playerCollectibles = {}; /* Indexed by player ID. */

function goToEpiloguesScreen() {
	$galleryEndingsScreen.show();
	$galleryCollectiblesScreen.hide();
	loadGalleryEndings();
	galleryGender(GALLERY_GENDER);
}

function goToCollectiblesScreen() {
	$galleryCollectiblesScreen.show();
	$galleryEndingsScreen.hide();
    updateLoadedCollectibles();
}

function loadGalleryScreen(){
	screenTransition($titleScreen, $galleryScreen);
    goToCollectiblesScreen();
}

function backGalleryScreen(){
	screenTransition($galleryScreen, $titleScreen);
}


function loadPlayerCollectibles(player) {
	if (!player.collectibles || playerCollectibles[player.id] !== undefined) {
		return;
	}
	
	playerCollectibles[player.id] = null;
	
	$.ajax({
		type: "GET",
		url: player.folder + 'collectibles.xml',
		dataType: "text",
		success: function(xml) {
			var collectiblesArray = [];
			$(xml).find('collectible').each(function () {
				collectiblesArray.push(new PlayerCollectible(player, $(this)));
			});
			
			playerCollectibles[player.id] = collectiblesArray;
            updateCollectiblesScreen();
		}
	});
}


function updateLoadedCollectibles() {
	for (var i=0; i<loadedOpponents.length; i++) {
		if (!loadedOpponents[i]) continue;
		loadPlayerCollectibles(loadedOpponents[i]);	
	}
	
	updateCollectiblesScreen();
}

function updateCollectiblesScreen() {	
	$collectibleListPane.empty();
	
	for (var i=0; i<loadedOpponents.length; i++) {
		var player = loadedOpponents[i];
		if (!player) continue;
		
		var colList = playerCollectibles[player.id];
		if (!colList) continue;
		
		for (var j=0; j<colList.length;j++) {
			$collectibleListPane.append(colList[j].listElement());
		}
	}
}


function loadGalleryEndings(){
	if(allEndings.length > 0){
		return;
	}
	
	for(var i=0; i<loadedOpponents.length; i++){
		if (loadedOpponents[i].ending) {
			loadedOpponents[i].endings.each(function () {
				var gending = new GEnding(loadedOpponents[i], this);
				
				allEndings.push( gending );
				switch(gending.gender){
					case 'male': maleEndings.push(gending);
					break;
					case 'female': femaleEndings.push(gending);
					break;
					default: anyEndings.push(gending);
					break;
				}
			});
		}
	}
}

function loadEndingThunbnail(element, ending){
	element.removeClass('empty-thumbnail');
	if (ending.unlocked()) {
		element.removeClass('unlocked-thumbnail');
		element.css('background-image','url(\''+ending.image+'\')');
	} else {
		element.css('background-image', '');
		element.addClass('unlocked-thumbnail');
	}
}

function loadThumbnails(){
	var i=0;
	for(; i<epp && epp*galleryPage+i<galleryEndings.length; i++){
		loadEndingThunbnail($galleryEndings.eq(i), galleryEndings[epp*galleryPage+i]);
	}
	for( ; i<epp; i++){
		$galleryEndings.eq(i).removeClass('unlocked-thumbnail');
		$galleryEndings.eq(i).addClass('empty-thumbnail');
		$galleryEndings.eq(i).css('background-image', 'none');
	}
}

function galleryGender(gender){
	GALLERY_GENDER = gender;
	$('.gallery-gender-button').css('opacity', 0.4);
	switch(gender){
		case 'male':
			galleryEndings = maleEndings;
			$('#gallery-gender-male').css('opacity', 1);
			break;
		case 'female':
			galleryEndings = femaleEndings;
			$('#gallery-gender-female').css('opacity', 1);
			break;
		case 'any':
			galleryEndings = anyEndings;
			$('#gallery-gender-any').css('opacity', 1);
			break;
		default:
			galleryEndings = allEndings;
			$('#gallery-gender-all').css('opacity', 1);
			break;
	}
	galleryPages = Math.ceil(galleryEndings.length/parseFloat(epp));
	galleryPage = 0;
	$galleryPrevButton.attr('disabled', true);
	if(galleryPages==1){
		$galleryNextButton.attr('disabled', true);
	}
	else{
		$galleryNextButton.attr('disabled', false);
	}
	loadThumbnails();
}

function galleryNextPage(){
	galleryPage++;
	loadThumbnails();
	$galleryEndings.css('opacity', '');
	$galleryPrevButton.attr('disabled', false);
	if(galleryPage+1==galleryPages){
		$galleryNextButton.attr('disabled', true);
	}
}

function galleryPrevPage(){
	galleryPage--;
	loadThumbnails();
	$galleryEndings.css('opacity', '');
	$galleryNextButton.attr('disabled', false);
	if(galleryPage==0){
		$galleryPrevButton.attr('disabled', true);
	}
}

function selectEnding(i) {
	selectedEnding = epp*galleryPage+i;
	var ending = galleryEndings[selectedEnding];

	if (!ending) {
		return;
	}
	
	if (ending.unlockHint) {
		$selectedEndingHint[0].show();
		$selectedEndingHint[1].html(ending.unlockHint);
	} else {
		$selectedEndingHint[0].hide();
	}

	if (ending.unlocked()) {
		$galleryStartButton.attr('disabled', false);
		chosenEpilogue = ending;
		$selectedEndingLabels[0].html(ending.title);
	} else {
		$galleryStartButton.attr('disabled', true);
		chosenEpilogue = -1;
		$selectedEndingLabels[0].html('');
	}
	
	$galleryEndings.css('opacity', '');
	$galleryEndings.eq(i).css('opacity', 1);
	loadEndingThunbnail($selectedEndingPreview, ending);
	$selectedEndingLabels[1].html(ending.player.label);
	$selectedEndingLabels[2].html(ending.gender);
	switch(ending.gender){
		case 'male':
			$selectedEndingLabels[2].removeClass('female-style');
			$selectedEndingLabels[2].addClass('male-style');
			break;
		case 'female':
			$selectedEndingLabels[2].removeClass('male-style');
			$selectedEndingLabels[2].addClass('female-style');
			break;
		default:
			$selectedEndingLabels[2].removeClass('female-style');
			$selectedEndingLabels[2].removeClass('male-style');
	}
}

function doEpilogueFromGallery(){
	if (!chosenEpilogue) {
		return;
	}
	
	var player = chosenEpilogue.player;
	
	fetchCompressedURL(
		'opponents/' + player.id + "/behaviour.xml",
		/* Success callback.
		 * 'this' is bound to the Opponent object.
		 */
		function(xml) {
			var $xml = $(xml);
			
			var endingElem = null;
			
			$xml.find('epilogue').each(function () {
				if ($(this).find('title').html() === chosenEpilogue.title && $(this).attr('gender') === chosenEpilogue.gender) {
					endingElem = this;
				}
			});
			
			if($nameField.val()){
				players[HUMAN_PLAYER].label = $nameField.val();
			} else {
				switch(chosenEpilogue.gender){
					case "male": players[HUMAN_PLAYER].label = "Mister"; break;
					case "female" : players[HUMAN_PLAYER].label = "Missy"; break;
					default: players[HUMAN_PLAYER].label = (players[HUMAN_PLAYER].gender=="male")?"Mister":"Missy";
				}
			}
			
			// function definition in spniEpilogue.js
			chosenEpilogue = parseEpilogue(player, endingElem);
		
			if (USAGE_TRACKING) {
				var usage_tracking_report = {
					'date': (new Date()).toISOString(),
					'commit': VERSION_COMMIT,
					'type': 'gallery',
					'session': sessionID,
					'userAgent': navigator.userAgent,
					'origin': getReportedOrigin(),
					'chosen': {
						'id': chosenEpilogue.player.id,
						'title': chosenEpilogue.title
					}
				};
		
				$.ajax({
					url: USAGE_TRACKING_ENDPOINT,
					method: 'POST',
					data: JSON.stringify(usage_tracking_report),
					contentType: 'application/json',
					error: function (jqXHR, status, err) {
						console.error("Could not send usage tracking report - error "+status+": "+err);
					},
				});
			}
		
			epilogueContainer.dataset.background = -1;
			epilogueContainer.dataset.scene = -1;
		
			loadEpilogue(chosenEpilogue); //initialise buttons and text boxes
			screenTransition($galleryScreen, $epilogueScreen); //currently transitioning from title screen, because this is for testing
			$epilogueSelectionModal.modal("hide");
		}
	);
}
