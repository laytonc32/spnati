/********************************************************************************
 This file contains the variables and functions that form the select screens of
 the game. The parsing functions for the opponent.xml file.
 ********************************************************************************/

/**********************************************************************
 *****               Opponent & Group Specification               *****
 **********************************************************************/
 
/**************************************************
 * Stores meta information about groups.
 **************************************************/
function createNewGroup (title) {
	var newGroupObject = {title:title,
						  opponents:Array(4)};

	return newGroupObject;
}

/**********************************************************************
 *****                  Select Screen UI Elements                 *****
 **********************************************************************/

/* main select screen */
$selectTable = $("#select-table");
$selectBubbles = [$("#select-bubble-1"),
                  $("#select-bubble-2"),
                  $("#select-bubble-3"),
                  $("#select-bubble-4")];
$selectDialogues = [$("#select-dialogue-1"),
                    $("#select-dialogue-2"),
                    $("#select-dialogue-3"),
                    $("#select-dialogue-4")];
$selectAdvanceButtons = [$("#select-advance-button-1"),
                         $("#select-advance-button-2"),
                         $("#select-advance-button-3"),
                         $("#select-advance-button-4")];
$selectImages = [$("#select-image-1"),
                 $("#select-image-2"),
                 $("#select-image-3"),
                 $("#select-image-4")];
$selectLabels = [$("#select-name-label-1"),
                 $("#select-name-label-2"),
                 $("#select-name-label-3"),
                 $("#select-name-label-4")];
$selectButtons = [$("#select-slot-button-1"),
                  $("#select-slot-button-2"),
                  $("#select-slot-button-3"),
                  $("#select-slot-button-4")];
$selectMainButton = $("#main-select-button");
$selectRandomButtons = $("#select-random-button, #select-random-female-button, #select-random-male-button");
$selectRandomTableButton = $("#select-random-group-button");
$selectRemoveAllButton = $("#select-remove-all-button");

$selectSuggestions = [
    $("#opponent-suggestions-1"),
    $("#opponent-suggestions-2"),
    $("#opponent-suggestions-3"),
    $("#opponent-suggestions-4"),
];

$suggestionQuads = [
    [$("#opponent-suggestion-1-1"), $("#opponent-suggestion-1-2"), $("#opponent-suggestion-1-3"), $("#opponent-suggestion-1-4")],
    [$("#opponent-suggestion-2-1"), $("#opponent-suggestion-2-2"), $("#opponent-suggestion-2-3"), $("#opponent-suggestion-2-4")],
    [$("#opponent-suggestion-3-1"), $("#opponent-suggestion-3-2"), $("#opponent-suggestion-3-3"), $("#opponent-suggestion-3-4")],
    [$("#opponent-suggestion-4-1"), $("#opponent-suggestion-4-2"), $("#opponent-suggestion-4-3"), $("#opponent-suggestion-4-4")],
]

/* individual select screen */
$individualSelectTable = $("#individual-select-table");
$individualNameLabels = [$("#individual-name-label-1"), $("#individual-name-label-2"), $("#individual-name-label-3"), $("#individual-name-label-4")];
$individualPrefersLabels = [$("#individual-prefers-label-1"), $("#individual-prefers-label-2"), $("#individual-prefers-label-3"), $("#individual-prefers-label-4")];
$individualSexLabels = [$("#individual-sex-label-1"), $("#individual-sex-label-2"), $("#individual-sex-label-3"), $("#individual-sex-label-4")];
$individualHeightLabels = [$("#individual-height-label-1"), $("#individual-height-label-2"), $("#individual-height-label-3"), $("#individual-height-label-4")];
$individualSourceLabels = [$("#individual-source-label-1"), $("#individual-source-label-2"), $("#individual-source-label-3"), $("#individual-source-label-4")];
$individualWriterLabels = [$("#individual-writer-label-1"), $("#individual-writer-label-2"), $("#individual-writer-label-3"), $("#individual-writer-label-4")];
$individualArtistLabels = [$("#individual-artist-label-1"), $("#individual-artist-label-2"), $("#individual-artist-label-3"), $("#individual-artist-label-4")];
$individualCountBoxes = [$("#individual-counts-1"), $("#individual-counts-2"), $("#individual-counts-3"), $("#individual-counts-4")];
$individualLineCountLabels = [$("#individual-line-count-label-1"), $("#individual-line-count-label-2"), $("#individual-line-count-label-3"), $("#individual-line-count-label-4")];
$individualPoseCountLabels = [$("#individual-pose-count-label-1"), $("#individual-pose-count-label-2"), $("#individual-pose-count-label-3"), $("#individual-pose-count-label-4")];
$individualDescriptionLabels = [$("#individual-description-label-1"), $("#individual-description-label-2"), $("#individual-description-label-3"), $("#individual-description-label-4")];
$individualBadges = [$("#individual-badge-1"), $("#individual-badge-2"), $("#individual-badge-3"), $("#individual-badge-4")];
$individualStatuses = [$("#individual-status-1"), $("#individual-status-2"), $("#individual-status-3"), $("#individual-status-4")];
$individualLayers = [$("#individual-layer-1"), $("#individual-layer-2"), $("#individual-layer-3"), $("#individual-layer-4")];

$individualImages = [$("#individual-image-1"), $("#individual-image-2"), $("#individual-image-3"), $("#individual-image-4")];
$individualButtons = [$("#individual-button-1"), $("#individual-button-2"), $("#individual-button-3"), $("#individual-button-4")];

$individualPageIndicator = $("#individual-page-indicator");
$individualMaxPageIndicator = $("#individual-max-page-indicator");

$individualCreditsButton = $('#individual-credits-button');

/* group select screen */
$groupSelectTable = $("#group-select-table");
$groupSwitchTestingButton = $("#group-switch-testing-button");
$groupNameLabels = [$("#group-name-label-1"), $("#group-name-label-2"), $("#group-name-label-3"), $("#group-name-label-4")];
$groupPrefersLabels = [$("#group-prefers-label-1"), $("#group-prefers-label-2"), $("#group-prefers-label-3"), $("#group-prefers-label-4")];
$groupSexLabels = [$("#group-sex-label-1"), $("#group-sex-label-2"), $("#group-sex-label-3"), $("#group-sex-label-4")];
$groupHeightLabels = [$("#group-height-label-1"), $("#group-height-label-2"), $("#group-height-label-3"), $("#group-height-label-4")];
$groupSourceLabels = [$("#group-source-label-1"), $("#group-source-label-2"), $("#group-source-label-3"), $("#group-source-label-4")];
$groupWriterLabels = [$("#group-writer-label-1"), $("#group-writer-label-2"), $("#group-writer-label-3"), $("#group-writer-label-4")];
$groupArtistLabels = [$("#group-artist-label-1"), $("#group-artist-label-2"), $("#group-artist-label-3"), $("#group-artist-label-4")];
$groupCountBoxes = [$("#group-counts-1"), $("#group-counts-2"), $("#group-counts-3"), $("#group-counts-4")];
$groupLineCountLabels = [$("#group-line-count-label-1"), $("#group-line-count-label-2"), $("#group-line-count-label-3"), $("#group-line-count-label-4")];
$groupPoseCountLabels = [$("#group-pose-count-label-1"), $("#group-pose-count-label-2"), $("#group-pose-count-label-3"), $("#group-pose-count-label-4")];
$groupDescriptionLabels = [$("#group-description-label-1"), $("#group-description-label-2"), $("#group-description-label-3"), $("#group-description-label-4")];
$groupBadges = [$("#group-badge-1"), $("#group-badge-2"), $("#group-badge-3"), $("#group-badge-4")];
$groupStatuses = [$("#group-status-1"), $("#group-status-2"), $("#group-status-3"), $("#group-status-4")];
$groupLayers = [$("#group-layer-1"), $("#group-layer-2"), $("#group-layer-3"), $("#group-layer-4")];

$groupImages = [$("#group-image-1"), $("#group-image-2"), $("#group-image-3"), $("#group-image-4")];
$groupNameLabel = $("#group-name-label");
$groupButton = $("#group-button");

$groupPageIndicator = $("#group-page-indicator");
$groupMaxPageIndicator = $("#group-max-page-indicator");

$groupCreditsButton = $('#group-credits-button');

$searchName = $("#search-name");
$searchSource = $("#search-source");
$searchTag = $("#search-tag");
$searchGenderOptions = [$("#search-gender-1"), $("#search-gender-2"), $("#search-gender-3")];

$sortingOptionsItems = $(".sort-dropdown-options li");

$groupSearchGroupName = $("#group-search-group-name");
$groupSearchName = $("#group-search-name");
$groupSearchSource = $("#group-search-source");
$groupSearchTag = $("#group-search-tag");
$groupSearchGenderOptions = [$("#group-search-gender-1"), $("#group-search-gender-2"), $("#group-search-gender-3"), $("#group-search-gender-4")];

/**********************************************************************
 *****                  Select Screen Variables                   *****
 **********************************************************************/

/* hidden variables */
var mainSelectHidden = false;
var singleSelectHidden = false;
var groupSelectHidden = false;

/* opponent listing file */
var listingFile = "opponents/listing.xml";
var metaFile = "meta.xml";

/* opponent information storage */
var loadedOpponents = [];
var selectableOpponents = loadedOpponents;
var hiddenOpponents = [];
var loadedGroups = [[], []];
var selectableGroups = [loadedGroups[0], loadedGroups[1]];
var loadingOpponents = Array(4);

/* page variables */
var groupSelectScreen = 0;
var individualPage = 0;
var groupPage = [0, 0];
var chosenGender = -1;
var chosenGroupGender = -1;
var sortingMode = "Featured";
var sortingOptionsMap = {
    "Newest" : sortOpponentsByMultipleFields("-release"),
    "Oldest" : sortOpponentsByMultipleFields("release"),
    "Most Layers" : sortOpponentsByMultipleFields("-layers"),
    "Fewest Layers" : sortOpponentsByMultipleFields("layers"),
    "Name (A-Z)" : sortOpponentsByMultipleFields("first", "last"),
    "Name (Z-A)" : sortOpponentsByMultipleFields("-first", "-last"),
    "Targeted most by selected" : sortOpponentsByMostTargeted(),
};
var individualCreditsShown = false;
var groupCreditsShown = false;

/* consistence variables */
var selectedSlot = 0;
var shownIndividuals = Array(4);
var shownGroup = Array(4);
var shownSuggestions = [Array(4), Array(4), Array(4), Array(4)];
var randomLock = false;

/* Status icon tooltips */
var TESTING_STATUS_TOOLTIP = "This opponent is currently in testing.";
var OFFLINE_STATUS_TOOLTIP = "This opponent has been retired from the official version of the game.";
var INCOMPLETE_STATUS_TOOLTIP = "This opponent is incomplete and currently not in development.";

/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Loads all of the content required to display the title
 * screen.
 ************************************************************/
function loadSelectScreen () {
    loadListingFile();

	updateSelectionVisuals();
}

/************************************************************
 * Loads and parses the main opponent listing file.
 ************************************************************/
function loadListingFile () {
	/* clear the previous meta information */
	var outstandingLoads = 0;
	var opponentGroupMap = {};
	var opponentMap = {};
	var onComplete = function(opp, index) {
		if (opp) {
			if (opp.id in opponentMap) {
				loadedOpponents[opponentMap[opp.id]] = opp;
			}
			if (opp.id in opponentGroupMap) {
				opponentGroupMap[opp.id].forEach(function(groupPos) {
					groupPos.group.opponents[groupPos.idx] = opp;
				});
			}
		}
		if (--outstandingLoads % 16 == 0) {
			updateSelectableOpponents();
			updateIndividualSelectScreen();
			updateSelectableGroups(0);
			updateSelectableGroups(1);
			updateGroupSelectScreen();
		}
	}

	/* grab and parse the opponent listing file */
	$.ajax({
        type: "GET",
		url: listingFile,
		dataType: "text",
		success: function(xml) {
            var $xml = $(xml);

			/* start by parsing the group descriptions to know which
			 * characters to load in addition to the main roster and any included statuses */
			$xml.find('groups>group').each(function () {
				var title = $(this).attr('title');
				var opp1 = $(this).attr('opp1');
				var opp2 = $(this).attr('opp2');
				var opp3 = $(this).attr('opp3');
				var opp4 = $(this).attr('opp4');

                var ids = [opp1, opp2, opp3, opp4];

				var newGroup = createNewGroup(title);
				ids.forEach(function(id, idx) {
					if (!(id in opponentGroupMap)) {
						opponentGroupMap[id] = [];
					}
					opponentGroupMap[id].push({ group: newGroup, idx: idx });
				});
				loadedGroups[$(this).attr('testing') ? 1 : 0].push(newGroup);
			});

            /* now actually load the characters */
            var oppDefaultIndex = 0; // keep track of an opponent's default placement

            $individualListings = $xml.find('individuals');
            $individualListings.find('opponent').each(function () {
                var oppStatus = $(this).attr('status');
                var id = $(this).text();
                var releaseNumber = $(this).attr('release');
                var doInclude = (oppStatus === undefined || includedOpponentStatuses[oppStatus]);

                if (doInclude || id in opponentGroupMap) {
                    outstandingLoads++;
					if (doInclude) {
						opponentMap[id] = oppDefaultIndex++;
					}
                    loadOpponentMeta(id, oppStatus, releaseNumber, onComplete);
                }
            });

		}
	});
}

/************************************************************
 * Loads and parses the meta XML file of an opponent.
 ************************************************************/
function loadOpponentMeta (id, status, releaseNumber, onComplete) {
	/* grab and parse the opponent meta file */
    console.log("Loading metadata for \""+id+"\"");
	$.ajax({
        type: "GET",
		url: 'opponents/' + id + '/' + metaFile,
		dataType: "text",
		success: function(xml) {
            var $xml = $(xml);

			var opponent = new Opponent(id, $xml, status, releaseNumber);

			/* add the opponent to the list */
            onComplete(opponent);
		},
		error: function(err) {
			console.log("Failed reading \""+id+"\"");
			onComplete();
		}
	});
}

function updateStatusIcon(elem, status) {
    var icon_img = 'img/testing-badge.png';
    var tooltip = TESTING_STATUS_TOOLTIP;
    
    if(!status) {
        elem.removeAttr('title').removeAttr('data-original-title').hide();
        return;
    }
    
    if (status === 'offline') {
        icon_img = 'img/offline-badge.png';
        tooltip = OFFLINE_STATUS_TOOLTIP;
    } else if (status === 'incomplete') {
        icon_img = 'img/incomplete-badge.png';
        tooltip = INCOMPLETE_STATUS_TOOLTIP;
    }
    
    elem.attr({
        'src': icon_img,
        'title': tooltip,
        'data-original-title': tooltip,
    }).show().tooltip({
        'placement': 'left'
    });
}

/************************************************************
 * Loads opponents onto the individual select screen based
 * on the currently selected page.
 ************************************************************/
function updateIndividualSelectScreen () {
	/* safety wrap around */
	if (individualPage < 0) {
		/* wrap to last page */
		individualPage = Math.ceil(selectableOpponents.length/4)-1;
	}
	$individualPageIndicator.val(individualPage+1);

	/* keep track of how many opponents were on this screen */
	var empty = 0;

    /* create and load all of the individual opponents */
	for (var i = individualPage*4; i < (individualPage+1)*4; i++) {
		var index = i - individualPage*4;

		if (i < selectableOpponents.length) {
			shownIndividuals[index] = selectableOpponents[i];

			$individualNameLabels[index].html(selectableOpponents[i].first + " " + selectableOpponents[i].last);
			$individualPrefersLabels[index].html(selectableOpponents[i].label);
			$individualSexLabels[index].html(selectableOpponents[i].gender);
			$individualSourceLabels[index].html(selectableOpponents[i].source);
			$individualWriterLabels[index].html(wordWrapHtml(selectableOpponents[i].writer));
			$individualArtistLabels[index].html(wordWrapHtml(selectableOpponents[i].artist));
			$individualDescriptionLabels[index].html(selectableOpponents[i].description);

            if (EPILOGUE_BADGES_ENABLED && selectableOpponents[i].ending) {
                $individualBadges[index].show();
            }
            else {
                $individualBadges[index].hide();
            }
            
            updateStatusIcon($individualStatuses[index], selectableOpponents[i].status);
            
            $individualLayers[index].show();
            $individualLayers[index].attr("src", "img/layers" + selectableOpponents[i].layers + ".png");

			$individualImages[index].attr('src', selectableOpponents[i].folder + selectableOpponents[i].image);
			$individualImages[index].css('height', selectableOpponents[i].scale + '%');
			$individualImages[index].show();
			if (selectableOpponents[i].enabled == "true") {
				$individualButtons[index].html('Select Opponent');
				$individualButtons[index].attr('disabled', false);
			} else {
				$individualButtons[index].html('Coming Soon');
				$individualButtons[index].attr('disabled', true);
			}
		} else {
			delete shownIndividuals[index];

			$individualNameLabels[index].html("");
			$individualPrefersLabels[index].html("");
			$individualSexLabels[index].html("");
			$individualSourceLabels[index].html("");
			$individualWriterLabels[index].html("");
			$individualArtistLabels[index].html("");
            $individualCountBoxes[index].css("visibility", "hidden");
			$individualDescriptionLabels[index].html("");
            $individualBadges[index].hide();
            $individualStatuses[index].hide();
            $individualLayers[index].hide();

			$individualImages[index].hide();
			$individualButtons[index].attr('disabled', true);

			empty++;
		}
    }

	/* reload if the page is empty */
	if (empty == 4 && individualPage != 0) {
		individualPage = 0;
		updateIndividualSelectScreen();
	}
}

/************************************************************
 * Loads opponents onto the group select screen based on the
 * currently selected page.
 ************************************************************/
function updateGroupSelectScreen () {
	/* safety wrap around */
  if (groupPage[groupSelectScreen] < 0) {
		/* wrap to last page */
		groupPage[groupSelectScreen] = (selectableGroups[groupSelectScreen].length)-1;
	} else if (groupPage[groupSelectScreen] > selectableGroups[groupSelectScreen].length-1) {
		/* wrap to the first page */
		groupPage[groupSelectScreen] = 0;
	}
	$groupPageIndicator.val(groupPage[groupSelectScreen]+1);
    $groupMaxPageIndicator.html("of "+selectableGroups[groupSelectScreen].length);

    /* create and load all of the individual opponents */
	$groupButton.attr('disabled', false);
	for (var i = 0; i < 4; i++) {
		var opponent = selectableGroups[groupSelectScreen].length > 0 ?
            selectableGroups[groupSelectScreen][groupPage[groupSelectScreen]].opponents[i] :
            undefined;

		if (opponent && typeof opponent == "object") {
			shownGroup[i] = opponent;

			$groupNameLabels[i].html(opponent.first + " " + opponent.last);
			$groupPrefersLabels[i].html(opponent.label);
			$groupSexLabels[i].html(opponent.gender);
			$groupSourceLabels[i].html(opponent.source);
			$groupWriterLabels[i].html(wordWrapHtml(opponent.writer));
			$groupArtistLabels[i].html(wordWrapHtml(opponent.artist));
			$groupDescriptionLabels[i].html(opponent.description);

            if (EPILOGUE_BADGES_ENABLED && opponent.ending) {
                $groupBadges[i].show();
            }
            else {
                $groupBadges[i].hide();
            }
            
            updateStatusIcon($groupStatuses[i], opponent.status);
            
            $groupLayers[i].show();
            $groupLayers[i].attr("src", "img/layers" + opponent.layers + ".png");

			$groupImages[i].attr('src', opponent.folder + opponent.image);
			$groupImages[i].css('height', opponent.scale + '%');
			$groupImages[i].show();
		} else {
			delete shownGroup[i];

			$groupNameLabels[i].html("");
			$groupPrefersLabels[i].html("");
			$groupSexLabels[i].html("");
			$groupSourceLabels[i].html("");
			$groupWriterLabels[i].html("");
			$groupArtistLabels[i].html("");
			$groupDescriptionLabels[i].html("");
            $groupBadges[i].hide();
            $groupStatuses[i].hide();
            $groupLayers[i].hide();
			$groupImages[i].hide();
			$groupButton.attr('disabled', true);
		}
    }
    if (selectableGroups[groupSelectScreen].length == 0) {
        $groupNameLabel.html("(No matches)");
    } else {
        $groupNameLabel.html(selectableGroups[groupSelectScreen][groupPage[groupSelectScreen]].title);
    }
}

/* Sets the suggested opponent to be displayed in a given slot and quadrant.
 * Arguments:
 * - opponent: the opponent object to display
 * - slot: the selection slot to load into
 * - quad: the quadrant of said selection slot to load into
 */
function updateSuggestionQuad(slot, quad, opponent) {
    var img_elem = $suggestionQuads[slot][quad].children('.opponent-suggestion-image');
    var label_elem = $suggestionQuads[slot][quad].children('.opponent-suggestion-label');
    var tooltip = null;
    
    if (opponent.status === 'testing') {
        tooltip = TESTING_STATUS_TOOLTIP;
    } else if (opponent.status === 'offline') {
        tooltip = OFFLINE_STATUS_TOOLTIP;
    } else if (opponent.status === 'incomplete') {
        tooltip = INCOMPLETE_STATUS_TOOLTIP;
    }
    
    shownSuggestions[slot][quad] = opponent.id;
    
    img_elem.attr({
        'title': tooltip,
        'data-original-title': tooltip,
        'src': opponent.folder+opponent.image
    }).tooltip();
    
    label_elem.text(opponent.label);
}

/* Sets the given selection screen slot to display 4 opponents from an array.
 * Arguments:
 * - slot: the main select screen slot to update (zero-indexed)
 * - suggestionsArray: the array to draw suggestions from
 * - startIndex: the index into suggestionsArray to begin drawing suggestions from
 */
function updateSuggestions(slot, suggestionsArray, startIndex) {
    for(var i=0;i<4;i++) {
        if (suggestionsArray[startIndex+i]) {
            updateSuggestionQuad(slot, i, suggestionsArray[startIndex+i]);
        }
    }
}


/**********************************************************************
 *****                   Interaction Functions                    *****
 **********************************************************************/

/************************************************************
 * Filters the list of selectable opponents based on those
 * already selected and performs search and sort logic.
 ************************************************************/
function updateSelectableOpponents(autoclear) {
    var name = $searchName.val().toLowerCase();
    var source = $searchSource.val().toLowerCase();
    var tag = $searchTag.val().toLowerCase();

    // Array.prototype.filter automatically skips empty slots
    selectableOpponents = loadedOpponents.filter(function(opp) {
        // filter by name
        if (name
            && opp.label.toLowerCase().indexOf(name) < 0
            && opp.first.toLowerCase().indexOf(name) < 0
            && opp.last.toLowerCase().indexOf(name) < 0) {
            return false;
        }

        // filter by source
        if (source && opp.source.toLowerCase().indexOf(source) < 0) {
            return false;
        }

        // filter by tag
        if (tag) {
            if (!opp.tags || !opp.tags.some(function(t) {
                return t.toLowerCase().indexOf(tag) >= 0;
            })) {
                return false;
            }
        }

        // filter by gender
        if ((chosenGender == 2 && opp.gender !== eGender.MALE)
            || (chosenGender == 3 && opp.gender !== eGender.FEMALE)) {
            return false;
        }

        /* hide selected opponents */
        if (players.some(function(p) { return p && p.id == opp.id; })) {
            return false;
        }
        
        if (loadingOpponents.some(function(p) { return p && p === opp.id; })) {
            return false;
        }

        return true;
    });

    // If a unique match was made, automatically clear the search so
    // another opponent can be found more quickly.
    if (autoclear && (name != null || source != null) && selectableOpponents.length == 0) {
        clearSearch();
        return;
    }

    /* sort opponents */
    // Since selectableOpponents is always reloaded here with featured order,
    // check if a different sorting mode is selected, and if yes, sort it.
    if (sortingOptionsMap.hasOwnProperty(sortingMode)) {
        selectableOpponents.sort(sortingOptionsMap[sortingMode]);
    }

    /* update max page indicator */
    $individualMaxPageIndicator.html("of "+Math.ceil(selectableOpponents.length/4));
}

/************************************************************
 * The player clicked on a suggested character button.
 ************************************************************/
function suggestionSelected(slot, quad) {
    var selectedID = shownSuggestions[slot-1][quad-1];
    
    if(!selectedID) {
        /* This shouldn't happen. */
        console.error("Could not find suggested opponent ID for slot " + slot + " and quad " + quad);
        return;
    }
    
    /* Find the character they selected. */
    for (var i=0; i<loadedOpponents.length; i++) {
        if (loadedOpponents[i].id === selectedID) {
            players[slot] = null;
            loadingOpponents[slot-1] = selectedID;
            
        	updateSelectionVisuals();
            
            loadedOpponents[i].loadBehaviour(playerLoadedCallback, slot);
            
            return;
        }
    }
    
    /* This shouldn't happen, either. */
    console.error("Could not find opponent with ID " + selectedID);
}

/************************************************************
 * The player clicked on an opponent slot.
 ************************************************************/
function selectOpponentSlot (slot) {
    if (!(slot in players)) {
        /* add a new opponent */
        selectedSlot = slot;

        /* Make sure the user doesn't have target-count sorting set if
         * the amount of loaded opponents drops to 0. */
        if (sortingMode === "Targeted most by selected") {
            var player_count = countLoadedOpponents();
            if (player_count <= 1) { 
                setSortingMode("Featured");
            }
        }

		/* update the list of selectable opponents based on those that are already selected, search, and sort options */
		updateSelectableOpponents(true);

		/* reload selection screen */
		updateIndividualSelectScreen();
        updateIndividualCountStats();

        /* switch screens */
		screenTransition($selectScreen, $individualSelectScreen);
    } else {
        /* remove the opponent that's there */
        $selectImages[slot-1].off('load');
        delete players[slot];
        
        updateSelectionVisuals();
    }
}

/************************************************************
 * The player clicked on the Preset Tables or Testing Tables button.
 ************************************************************/
function clickedSelectGroupButton (screen) {
    switchSelectGroupScreen(screen)

	/* switch screens */
	screenTransition($selectScreen, $groupSelectScreen);
}

/************************************************************
 * The player clicked on the Preset Tables or Testing Tables 
 * button from within the table select screen.
 ************************************************************/
function switchSelectGroupScreen (screen) {
    if (screen !== undefined) {
        groupSelectScreen = screen;
    } else {
        groupSelectScreen = 1 - groupSelectScreen;
    }
    if (groupSelectScreen == 1) {
        $groupSwitchTestingButton.html("Preset Tables");
    } else {
        $groupSwitchTestingButton.html("Testing Tables");
    }
    updateSelectableGroups(groupSelectScreen);
    updateGroupSelectScreen();
}

/************************************************************
 * Filters the list of selectable opponents based on those
 * already selected and performs search and sort logic.
 ************************************************************/
function updateSelectableGroups(screen) {
    var groupname = $groupSearchGroupName.val().toLowerCase();
    var name = $groupSearchName.val().toLowerCase();
    var source = $groupSearchSource.val().toLowerCase();
    var tag = $groupSearchTag.val().toLowerCase();

    // reset filters
    selectableGroups[screen] = loadedGroups[screen].filter(function(group) {
        if (!group.opponents.every(function(opp) { return opp; })) return false;

        if (groupname && group.title.toLowerCase().indexOf(groupname) < 0) return false;

        if (name && !group.opponents.some(function(opp) {
            return opp.label.toLowerCase().indexOf(name) >= 0
                || opp.first.toLowerCase().indexOf(name) >= 0
                || opp.last.toLowerCase().indexOf(name) >= 0;
        })) return false;

        if (source && !group.opponents.some(function(opp) {
            return opp.source.toLowerCase().indexOf(source) >= 0;
        })) return false;

        if ((chosenGroupGender == 2 || chosenGroupGender == 3)
            && !group.opponents.every(function(opp) {
                return opp.gender == (chosenGroupGender == 2 ? eGender.MALE : eGender.FEMALE);
            })) return false;

        if (chosenGroupGender == 4
            && !(group.opponents.some(function(opp) { return opp.gender == eGender.MALE; })
                 && group.opponents.some(function(opp) { return opp.gender == eGender.FEMALE; })))
            return false;

        return true;
    })
}


/************************************************************
 * The player clicked on the select random group slot.
 ************************************************************/
function clickedRandomGroupButton () {
	selectedSlot = 1;

    for (var i = 1; i < players.length; i++) {
        players[i] = null;
        $selectImages[i-1].off('load');
    }

	/* get a random number for the group listings */
  var randomGroupNumber = getRandomNumber(0, loadedGroups[0].length);
  var chosenGroup = loadedGroups[0][randomGroupNumber];
	console.log(chosenGroup.title);

	for (var i = 0; i < chosenGroup.opponents.length; i++) {
		/* Don't try to load empty character slots */
		if (!chosenGroup.opponents[i] || typeof chosenGroup.opponents[i] !== 'object') {
			continue;
		}

		/* character exists? Okay, load it */
		chosenGroup.opponents[i].loadBehaviour(playerLoadedCallback, i+1);
	}

	updateSelectionVisuals();
}

/************************************************************
 * The player clicked on the all random button.
 ************************************************************/
function clickedRandomFillButton (predicate) {
	/* compose a copy of the loaded opponents list */
	var loadedOpponentsCopy = loadedOpponents.filter(function(opp) {
        // Filter out already selected characters
        return (!players.some(function(p) { return p && p.id == opp.id; })
                && (!predicate || predicate(opp)));
    });

	/* select random opponents */
	for (var i = 1; i < players.length; i++) {
		/* if slot is empty */
		if (!(i in players)) {
			players[i] = null;

			/* select random opponent */
			var randomOpponent = getRandomNumber(0, loadedOpponentsCopy.length);

			/* load opponent */
            loadedOpponentsCopy[randomOpponent].loadBehaviour(playerLoadedCallback, i);

			/* remove random opponent from copy list */
			loadedOpponentsCopy.splice(randomOpponent, 1);
		}
	}

	updateSelectionVisuals();
}

/************************************************************
 * The player clicked on the remove all button.
 ************************************************************/
function clickedRemoveAllButton ()
{
    for (var i = 1; i < 5; i++) {
        delete players[i];
        $selectImages[i-1].off('load');
    }
    updateSelectionVisuals();
}

/************************************************************
 * The player clicked on a change stats card button on the
 * individual select screen.
 ************************************************************/
function changeIndividualStats (target) {
    for (var i = 1; i < 5; i++) {
        for (var j = 1; j < 4; j++) {
            if (j != target) {
                $('#individual-stats-page-'+i+'-'+j).hide();
            }
            else {
                $('#individual-stats-page-'+i+'-'+j).show();
            }
        }
    }

    individualCreditsShown = (target == 2); // true when Credits button is clicked
}

/************************************************************
 * The player clicked the select opponent button on the
 * individual select screen.
 ************************************************************/
function selectIndividualOpponent (slot) {
    /* move the stored player into the selected slot and update visuals */
	players[selectedSlot] = null;
    loadingOpponents[selectedSlot-1] = shownIndividuals[slot-1].id;
    
	updateSelectionVisuals();
	shownIndividuals[slot-1].loadBehaviour(playerLoadedCallback, selectedSlot);
	/* switch screens */
	screenTransition($individualSelectScreen, $selectScreen);
}

/************************************************************
 * This callback is called after an opponent's behaviour file is loaded,
 * in all selection cases: indiv. select, group select, random selection, etc.
 ************************************************************/
function playerLoadedCallback (playerObject, slot) {
    console.log(slot+": "+playerObject);
    players[slot] = playerObject;
    delete loadingOpponents[slot-1];
	updateBehaviour(slot, SELECTED);
	updateSelectionVisuals();
}

/************************************************************
 * The player is changing the page on the individual screen.
 ************************************************************/
function changeIndividualPage (skip, page) {
    console.log("resigtered");
    if (skip) {
        if (page == -1) {
            /* go to first page */
            individualPage = 0;
        } else if (page == 1) {
            /* go to last page */
            individualPage = Math.ceil(selectableOpponents.length/4)-1;
        } else {
            /* go to selected page */
            individualPage = Number($individualPageIndicator.val()) - 1;
        }
    } else {
        individualPage += page;
    }

    updateIndividualSelectScreen();
    updateIndividualCountStats();
}

/************************************************************
 * The player clicked on a change stats card button on the
 * group select screen.
 ************************************************************/
function changeGroupStats (target) {
    for (var i = 1; i < 5; i++) {
        for (var j = 1; j < 4; j++) {
            if (j != target) {
                $('#group-stats-page-'+i+'-'+j).hide();
            }
            else {
                $('#group-stats-page-'+i+'-'+j).show();
            }
        }
    }

    groupCreditsShown = (target == 2); // true when Credits button is clicked
}

/************************************************************
 * The player clicked the select opponent button on the
 * group select screen.
 ************************************************************/
function selectGroup () {
    /* clear the selection screen */
	for (var i = 1; i < 5; i++) {
		players[i] = null;
	}
	updateSelectionVisuals();

	/* load the group members */
	for (var i = 0; i < 4; i++) {
        var member = selectableGroups[groupSelectScreen][groupPage[groupSelectScreen]].opponents[i];
        if (member) {
            member.loadBehaviour(playerLoadedCallback, i+1);
		}
	}
    /* switch screens */
	screenTransition($groupSelectScreen, $selectScreen);
}

/************************************************************
 * The player is changing the page on the group screen.
 ************************************************************/
function changeGroupPage (skip, page) {
	if (skip) {
		if (page == -1) {
			/* go to first page */
      groupPage[groupSelectScreen] = 0;
		} else if (page == 1) {
			/* go to last page */
			groupPage[groupSelectScreen] = selectableGroups[groupSelectScreen].length-1;
		} else {
			/* go to selected page */
			groupPage[groupSelectScreen] = Number($groupPageIndicator.val()) - 1;
		}
	} else {
		groupPage[groupSelectScreen] += page;
	}
	updateGroupSelectScreen();
    updateGroupCountStats();
}

/************************************************************
 * The player clicked on the back button on the individual or
 * group select screen.
 ************************************************************/
function backToSelect () {
    /* switch screens */
	screenTransition($individualSelectScreen, $selectScreen);
	screenTransition($groupSelectScreen, $selectScreen);
}

/************************************************************
 * The player clicked on the start game button on the main
 * select screen.
 ************************************************************/
function advanceSelectScreen () {
    console.log("Starting game...");

    gameID = generateRandomID();

    if (USAGE_TRACKING) {
        var usage_tracking_report = {
            'date': (new Date()).toISOString(),
            'type': 'start_game',
            'session': sessionID,
            'game': gameID,
            'userAgent': navigator.userAgent,
            'origin': getReportedOrigin(),
            'table': {}
        };
        
        for (let i=1;i<5;i++) {
            if (players[i]) {
                usage_tracking_report.table[i] = players[i].id;
            }
        }
        
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

    advanceToNextScreen($selectScreen);
}

/************************************************************
 * The player clicked on the back button on the main select
 * screen.
 ************************************************************/
function backSelectScreen () {
	screenTransition($selectScreen, $titleScreen);
}

/**********************************************************************
 *****                     Display Functions                      *****
 **********************************************************************/

/************************************************************
 * Displays all of the current players on the main select
 * screen.
 ************************************************************/
function updateSelectionVisuals () {
    /* update all opponents */
    for (var i = 1; i < players.length; i++) {
        if (players[i]) {            
            /* update dialogue */
            $selectDialogues[i-1].html(players[i].chosenState.dialogue);

            /* update image */
            if (players[i].folder + players[i].chosenState.image
                != $selectImages[i-1].attr('src')) {
                var slot = i;
                $selectImages[i-1].attr('src', players[i].folder + players[i].chosenState.image);
                $selectImages[i-1].one('load', function() {
                    $selectBubbles[slot-1].show();
                    $selectImages[slot-1].css('height', players[slot].scale + '%');
                    $selectImages[slot-1].show();
                });
            } else {
                $selectBubbles[i-1].show();
                $selectImages[i-1].show();
            }

            /* update label */
            $selectLabels[i-1].html(players[i].label.initCap());

            /* change the button */
            $selectButtons[i-1].html("Remove Opponent");
            $selectButtons[i-1].removeClass("smooth-button-green");
            $selectButtons[i-1].addClass("smooth-button-red");
        } else {
            /* clear the view */
            $selectDialogues[i-1].html("");
            $selectAdvanceButtons[i-1].css({opacity : 0});
			$selectBubbles[i-1].hide();
			$selectImages[i-1].hide();
            $selectLabels[i-1].html("Opponent "+i);

            /* change the button */
            $selectButtons[i-1].html("Select Opponent");
            $selectButtons[i-1].removeClass("smooth-button-red");
            $selectButtons[i-1].addClass("smooth-button-green");
        }
    }

    /* Check to see if all opponents are loaded.
       Note: Slots with null in them are waiting for a character to be loaded */
    var filled = 0, loaded = 0;
    players.forEach(function(p, idx) {
        if (idx > 0) {
            filled++;
            if (p === null) {
                $selectButtons[idx-1].html('Loading...');
            } else {
                loaded++;
            }
            $selectButtons[idx-1].attr('disabled', p === null);
        }
    });

    /* if enough opponents are selected, and all those are loaded, then enable progression */
    $selectMainButton.attr('disabled', filled < 2 || loaded < filled);

    /* if all slots are taken, disable fill buttons */
    $selectRandomButtons.attr('disabled', filled >= 4);

    /* if no opponents are loaded, disable remove all button */
    $selectRemoveAllButton.attr('disabled', filled <= 0 || loaded < filled);

    /* Disable buttons while loading is going on */
    $selectRandomTableButton.attr('disabled', loaded < filled);
    $groupButton.attr('disabled', loaded < filled);
    
    /* Update suggestions images. */
    var current_player_count = countLoadedOpponents();
    
    if (current_player_count >= 3) {
        var suggested_opponents = loadedOpponents.filter(function(opp) {
            /* hide selected opponents */
            if (players.some(function(p) { return p && p.id == opp.id; })) {
                return false;
            }
            
            if (loadingOpponents.some(function(p) { return p && p === opp.id; })) {
                return false;
            }
            
            return true;
        });
    
        /* sort opponents */
        suggested_opponents.sort(sortOpponentsByMostTargeted());
        
        var suggestion_idx = 0;
        for (var i=1;i<players.length;i++) {
            if (players[i] === undefined) {
                updateSuggestions(i-1, suggested_opponents, suggestion_idx);
                $selectSuggestions[i-1].show();
                suggestion_idx += 4;
            } else {
                $selectSuggestions[i-1].hide();
            }
        }
    } else {
        for (var i=0;i<4;i++) {
            $selectSuggestions[i].hide();
        }
    }
}



/************************************************************
 * This is the callback for the group clicked rows, it
 * updates information on the group screen.
 ************************************************************/
function updateGroupScreen (playerObject) {
    /* find a spot to store this player */
    for (var i = 0; i < storedGroup.length; i++) {
        if (!storedGroup[i]) {
            storedGroup[i] = playerObject;
            $groupLabels[i+1].html(playerObject.label);
            break;
        }
    }

	/* enable the button */
	$groupButton.attr('disabled', false);
}

/************************************************************
 * Hides the table on the single selection screen.
 ************************************************************/
function hideSelectionTable() {
    mainSelectHidden = !mainSelectHidden;
    if (mainSelectHidden) {
        $selectTable.hide();
    }
    else {
        $selectTable.show();
    }
}

/************************************************************
 * Hides the table on the single selection screen.
 ************************************************************/
function hideSingleSelectionTable() {
    singleSelectHidden = !singleSelectHidden;
    if (singleSelectHidden) {
        $individualSelectTable.hide();
    }
    else {
        $individualSelectTable.show();
    }
}

/************************************************************
 * Hides the table on the single group screen.
 ************************************************************/
function hideGroupSelectionTable() {
    groupSelectHidden = !groupSelectHidden;
    if (groupSelectHidden) {
        $groupSelectTable.hide();
    }
    else {
        $groupSelectTable.show();
    }
}

function openSearchModal() {
    $searchModal.modal('show');
}


function closeSearchModal() {
    // perform the search and sort logic
    updateSelectableOpponents();

    // update
    updateIndividualSelectScreen();
    updateIndividualCountStats();
}

function clearSearch() {
    $searchName.val(null);
    $searchTag.val(null);
    $searchSource.val(null);
    closeSearchModal();
}

function changeSearchGender(gender) {
    chosenGender = gender;
    setActiveOption($searchGenderOptions, gender);
}

function openGroupSearchModal() {
    $groupSearchModal.modal('show');
}

function closeGroupSearchModal() {
    // perform the search and sort logic
    updateSelectableGroups(groupSelectScreen);

    // update
    updateGroupSelectScreen();
    updateGroupCountStats();
}

function clearGroupSearch() {
    $groupSearchName.val(null);
    $groupSearchGroupName.val(null);
    $groupSearchTag.val(null);
    $groupSearchSource.val(null);
    closeGroupSearchModal();
}

function changeGroupSearchGender(gender) {
    chosenGroupGender = gender;
    setActiveOption($groupSearchGenderOptions, gender);
}


/************************************************************
 * Sorting Functions
 ************************************************************/

/**
 * Callback for Arrays.sort to sort an array of objects by the given field.
 * Prefixing "-" to a field will cause the sort to be done in reverse.
 * Examples:
 *   // sorts myArr by each element's first name (A-Z)
 *   myArr.sort(sortOpponentsByField("first"));
 *   // sorts myArr by each element's last name (Z-A)
 *   myArr.sort(sortOpponentsByField("-last"));
 */
function sortOpponentsByField(field) {
    // check for prefix
    var order = 1; // 1 = forward, -1 = reversed
    if (field[0] === "-") {
        order = -1;
        field = field.substr(1);
    }

    return function(opp1, opp2) {
        var compare = 0;
        if (opp1[field] < opp2[field]) {
            compare = -1;
        }
        else if (opp1[field] > opp2[field]) {
            compare = 1;
        }
        return order * compare;
    }
}

/**
 * Callback for Arrays.sort to sort an array of objects over multiple given fields.
 * Prefixing "-" to a field will cause the sort to be done in reverse.
 * This should allow more flexibility in the sorting order.
 * Example:
 *   // sorts myArr by each element's number of layers (low to high),
 *   // and for elements whose layers are equivalent, sort them by first name (Z-A)
 *   myArr.sort(sortOpponentsByMultipleFields("layers", "-first"));
 */
function sortOpponentsByMultipleFields() {
    var fields = arguments; // retrieve the args passed in
    return function(opp1, opp2) {
        var i = 0;
        var compare = 0;
        // if both elements have the same field, check the next ones
        while (compare === 0 && i < fields.length) {
            compare = sortOpponentsByField(fields[i])(opp1, opp2);
            i++;
        }
        return compare;
    }
}

/**
 * Special Callback for Arrays.sort to sort an array of opponents on
 * the total number of lines targeting them the currently selected
 * opponents have.
 */
function sortOpponentsByMostTargeted() {
	return function(opp1, opp2) {
		counts = [opp1, opp2].map(function(opp) {
			return players.reduce(function(sum, p) {
				if (p && p.targetedLines && opp.id in p.targetedLines) {
					sum += p.targetedLines[opp.id].count;
				}
				return sum;
			}, 0);
		});
		if (counts[0] > counts[1]) return -1;
		if (counts[0] < counts[1]) return 1;
		return 0;
	}
}

function setSortingMode(mode) {
    sortingMode = mode;
    $("#sort-dropdown-selection").html(sortingMode); // change the dropdown text to the selected option
    individualPage = 0; // reset the page number
}

/** Event handler for the sort dropdown options. Fires when user clicks on a dropdown item. */
$sortingOptionsItems.on("click", function(e) {
    setSortingMode($(this).find('a').html());
});

/************************************************************
 * Word wrapping Functions
 ************************************************************/

/**
 * Inserts a fixed-size HTML element with the specified text to allow the content
 * to be either word-wrapped (if the text is long and spaces are present)
 * or word-broken (if text is long and no spaces are present).
 */
function wordWrapHtml(text) {
    text = text || "&nbsp;";
    return "<table class=\"wrap-text\"><tr><td>" + text + "</td></tr></table>";
}

/************************************************************
 * Dynamic dialogue and image counting functions
 ************************************************************/

/** Event handler for the individual selection screen credits button. */
$individualCreditsButton.on('click', function(e) {
    updateIndividualCountStats()
});

/** Event handler for the group selection screen credits button. */
$groupCreditsButton.on('click', function(e) {
    updateGroupCountStats();
});

/**
 * Loads and displays the number of unique dialogue lines and the number of pose images
 * into the character's player object for those currently on the selection screen.
 * Only loads if the unique line count or image count is not known.
 */
function updateOpponentCountStats(opponentArr, uiElements) {
    opponentArr.forEach(function(opp, idx) {
        // load behaviour file if line/image count is not known
        if (opp && (opp.uniqueLineCount === undefined || opp.posesImageCount === undefined)) {
            uiElements.countBoxes[idx].css("visibility", "visible");

            // retrieve line and image counts
            if (DEBUG) {
                console.log("[LineImageCount] Fetching counts for " + opp.label + " in slot " + idx);
            }
            
            var countsPromise = new Promise(function (resolve, reject) {
                fetchCompressedURL(
                    opp.folder + 'behaviour.xml',
                    resolve, reject
                );
            });
            
            countsPromise.then(countLinesImages).then(function(response) {
                opp.uniqueLineCount = response.numUniqueLines;
                opp.posesImageCount = response.numPoses;

                // show line and image counts
                if (DEBUG) {
                    console.log("[LineImageCount] Loaded " + opp.label + " from behaviour: " +
                      opp.uniqueLineCount + " lines, " + opp.posesImageCount + " images");
                }
                uiElements.lineLabels[idx].html(opp.uniqueLineCount);
                uiElements.poseLabels[idx].html(opp.posesImageCount);
            });
        }
        else {
            // this character's counts were previously loaded
            if (opp) {
                if (DEBUG) {
                    console.log("[LineImageCount] Loaded previous count for " + opp.label + ": " +
                      opp.uniqueLineCount + " lines, " + opp.posesImageCount + " images)");
                }
                uiElements.countBoxes[idx].css("visibility", "visible");
                uiElements.lineLabels[idx].html(opp.uniqueLineCount);
                uiElements.poseLabels[idx].html(opp.posesImageCount);
            }
            else {
                // there is no character in the slot
                uiElements.countBoxes[idx].css("visibility", "hidden");
                uiElements.lineLabels[idx].html("");
                uiElements.poseLabels[idx].html("");
            }
        }
    });
}

/** Dialogue/image count update function for the individual selection screen. */
function updateIndividualCountStats() {
    if (individualCreditsShown) {
        var individualUIElements = {
            countBoxes : $individualCountBoxes,
            lineLabels : $individualLineCountLabels,
            poseLabels : $individualPoseCountLabels
        };
        updateOpponentCountStats(shownIndividuals, individualUIElements);
    }
}

/** Dialogue/image count update function for the group selection screen. */
function updateGroupCountStats() {
    if (groupCreditsShown) {
        var groupUIElements = {
            countBoxes : $groupCountBoxes,
            lineLabels : $groupLineCountLabels,
            poseLabels : $groupPoseCountLabels
        };
        updateOpponentCountStats(shownGroup, groupUIElements);
    }
}

/**
 * Callback to parse the number of lines of dialogue and number of images
 * given a character's behaviour XML. Returns the counts as an object with
 * properties numTotalLines, numUniqueLines, and numPoses.
 */
function countLinesImages(xml) {
    // parse all lines of dialogue and all images
	var numTotalLines = 0;
	var numUniqueDialogueLines = 0;
	var numUniqueUsedPoses = 0;
    var lines = {};
    var poses = {};
    $(xml).find('state').each(function(idx, data) {
		numTotalLines++;
		// count only unique lines of dialogue
		if (lines[data.textContent.trim()] === undefined) numUniqueDialogueLines++;
        lines[data.textContent.trim()] = 1;
		// count unique number of poses used in dialogue
		// note that this number may differ from actual image count if some images
		// are never used, or if images that don't exist are used in the dialogue
		if (poses[data.getAttribute("img")] === undefined) numUniqueUsedPoses++;
        poses[data.getAttribute("img")] = 1;
    });

    return {
        numTotalLines : numTotalLines,
        numUniqueLines : numUniqueDialogueLines,
        numPoses : numUniqueUsedPoses
    };
}
