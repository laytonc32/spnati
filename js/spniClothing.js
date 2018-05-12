/********************************************************************************
 This file contains the variables and functions that store information on player 
 clothing and player stripping.
 ********************************************************************************/
 
/**********************************************************************
 *****                Clothing Object Specification               *****
 **********************************************************************/
 
/* clothing types */
var IMPORTANT_ARTICLE = "important";
var MAJOR_ARTICLE = "major";
var MINOR_ARTICLE = "minor";
var EXTRA_ARTICLE = "extra";

/* clothing positions */
var UPPER_ARTICLE = "upper";
var LOWER_ARTICLE = "lower";
var OTHER_ARTICLE = "other";

var STAGE_ALIVE = 0;
var STAGE_NAKED = -3;
var STAGE_MASTURBATING = -2;
var STAGE_FINISHED = -1;

/************************************************************
 * Stores information on an article of clothing.
 ************************************************************/
function createNewClothing (proper, lower, type, position, image, layer, id) {
	var newClothingObject = {proper:proper, 
						     lower:lower, 
						     type:type, 
						     position:position,
                             image:image,
							 layer:layer,
							 id:id};
						  
	return newClothingObject;
}

/**********************************************************************
 *****                    Stripping Variables                     *****
 **********************************************************************/
 
/* stripping modal */
$stripModal = $("#stripping-modal");
$stripClothing = $("#stripping-clothing-area");
$stripButton = $("#stripping-modal-button");

/* consistence */
var selectedClothing = 0;

/**********************************************************************
 *****                      Strip Functions                       *****
 **********************************************************************/
 
 /************************************************************
 * Fetches the appropriate dialogue trigger for the provided
 * article of clothing, based on whether the article is going 
 * to be removed or has been removed. Written to prevent duplication.
 ************************************************************/
function getClothingTrigger (player, clothing, removed) {
	var type = clothing.type;
	var pos = clothing.position;
	var gender = players[player].gender;
	var size = players[player].size;

	/* starting with important articles */
	if (type == IMPORTANT_ARTICLE) {
		if (pos == UPPER_ARTICLE) {
			if (gender == eGender.MALE) {
				if (removed) {
					return MALE_CHEST_IS_VISIBLE;
				} else {
					return MALE_CHEST_WILL_BE_VISIBLE;
				}
			} else if (gender == eGender.FEMALE) {
				if (removed) {
					if (size == eSize.LARGE) {
						return FEMALE_LARGE_CHEST_IS_VISIBLE;
					} else if (size == eSize.SMALL) {
						return FEMALE_SMALL_CHEST_IS_VISIBLE;
					} else {
						return FEMALE_MEDIUM_CHEST_IS_VISIBLE;
					}
				} else {
					return FEMALE_CHEST_WILL_BE_VISIBLE;
				}
			}
		} else if (pos == LOWER_ARTICLE) {
			if (gender == eGender.MALE) {
				if (removed) {
					if (size == eSize.LARGE) {
						return MALE_LARGE_CROTCH_IS_VISIBLE;
					} else if (size == eSize.SMALL) {
						return MALE_SMALL_CROTCH_IS_VISIBLE;
					} else {
						return MALE_MEDIUM_CROTCH_IS_VISIBLE;
					}
				} else {
					return MALE_CROTCH_WILL_BE_VISIBLE;
				}
			} else if (gender == eGender.FEMALE) {
				if (removed) {
					return FEMALE_CROTCH_IS_VISIBLE;
				} else {
					return FEMALE_CROTCH_WILL_BE_VISIBLE;
				}
			}
		} else {
			/* this shouldn't happen, but if it does then just pretend it's a major article */
			if (gender == eGender.MALE) {
				if (removed) {
					return MALE_REMOVED_MAJOR;
				} else {
					return MALE_REMOVING_MAJOR;
				}
			} else if (gender == eGender.FEMALE) {
				if (removed) {
					return FEMALE_REMOVED_MAJOR;
				} else {
					return FEMALE_REMOVING_MAJOR;
				}
			}
		}
	}
	/* next major articles */
	else if (type == MAJOR_ARTICLE) {
		if (gender == eGender.MALE) {
			if (removed) {
				return MALE_REMOVED_MAJOR;
			} else {
				return MALE_REMOVING_MAJOR;
			}
		} else if (gender == eGender.FEMALE) {
			if (removed) {
				return FEMALE_REMOVED_MAJOR;
			} else {
				return FEMALE_REMOVING_MAJOR;
			}
		}
	}
	/* next minor articles */
	else if (type == MINOR_ARTICLE) {
		if (gender == eGender.MALE) {
			if (removed) {
				return MALE_REMOVED_MINOR;
			} else {
				return MALE_REMOVING_MINOR;
			}
		} else if (gender == eGender.FEMALE) {
			if (removed) {
				return FEMALE_REMOVED_MINOR;
			} else {
				return FEMALE_REMOVING_MINOR;
			}
		}
	}
	/* next accessories */
	else if (type == EXTRA_ARTICLE) {
		if (gender == eGender.MALE) {
			if (removed) {
				return MALE_REMOVED_ACCESSORY;
			} else {
				return MALE_REMOVING_ACCESSORY;
			}
		} else if (gender == eGender.FEMALE) {
			if (removed) {
				return FEMALE_REMOVED_ACCESSORY;
			} else {
				return FEMALE_REMOVING_ACCESSORY;
			}
		}
	}
}

/************************************************************
 * Determines whether or not the provided player is winning
 * or losing and returns the appropriate dialogue trigger.
 ************************************************************/
function determineStrippingSituation (player) {
	/* gather the clothing amounts of each player */
	var clothingCounts = [0, 0, 0, 0, 0];
	for (var i = 0; i < players.length; i++) {
        if (players[i]) {
            for (var j = 0; j < players[i].clothing.length; j++) {
                if (players[i].clothing[j]) {
                    clothingCounts[i]++;
                }
            }
        }
	}
	
	/* determine if this player's clothing count is the highest or lowest */
	var isMax = true;
	var isMin = true;
	
	for (var i = 0; i < players.length; i++) {
		if (i != player) {
			if (clothingCounts[player] > clothingCounts[i]) {
				isMin = false;
			} else if (clothingCounts[player] <= clothingCounts[i]) {
				isMax = false;
			}
		}
	}
	
	/* return appropriate trigger */
	if (isMax) {
		return PLAYER_MUST_STRIP_WINNING;
	} else if (isMin) {
		return PLAYER_MUST_STRIP_LOSING;
	} else {
		return PLAYER_MUST_STRIP_NORMAL;
	}
}

/************************************************************
 * Manages the dialogue triggers before a player strips or forfeits.
 ************************************************************/
function playerMustStrip (player) {
    /* count the clothing the player has remaining */
    /* assume the player only has IMPORTANT_ARTICLES */
    var importantOnly = true
    var positionIsUpper = false
    var positionIsLower = false
    var clothes = 0;
    for (var i = 0; i < players[player].clothing.length; i++) {
        if (players[player] && players[player].clothing[i]) {
            clothes++;

            /* determine if there is a non-important item of clothing worn */
            if (players[player].clothing[i].type !== IMPORTANT_ARTICLE) {
				importantOnly = false;
				console.log(players[player].clothing[i].type);
			
			} else {
               		/* determine if the posistion of an item of clothing is upper */
                if (players[player].clothing[i].position === UPPER_ARTICLE) {
					positionIsUpper = true;
					console.log(players[player].clothing[i].position);
                    /* determine the posistion of an item of clothing is lower */
				} else if (players[player].clothing[i].position === LOWER_ARTICLE) {
					   positionIsLower = true;
					   console.log(players[player].clothing[i].position);
					}
				}
			}
		}
	

    var startingClothes = players[player].clothing.length;
	
	if (clothes > 0) {
		/* the player has clothes and will strip */
		if (player == HUMAN_PLAYER) {
			if (players[HUMAN_PLAYER].gender == eGender.MALE) {
					if (clothes <2 && clothes > 0 &&importantOnly == true && positionIsLower == true) {
						updateAllBehaviours(player, MALE_CROTCH_WILL_BE_VISIBLE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
					} else {
						if (clothes <2 && clothes > 0 &&importantOnly == true && positionIsUpper == true) {
							updateAllBehaviours(player, MALE_CHEST_WILL_BE_VISIBLE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);	
						} 
						else {
							updateAllBehaviours(player, MALE_HUMAN_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
						}
					}							
			}
			else {
				if (clothes < 3 && clothes > 1 && importantOnly == true) {				
					updateAllBehaviours(player, FEMALE_HUMAN_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
				} 
				else {
					if (clothes < 2 && clothes > 0 && importantOnly == true && positionIsUpper == true) {
						updateAllBehaviours(player, FEMALE_CHEST_WILL_BE_VISIBLE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);	
					} 
					else {
						if (clothes < 2 && clothes > 0 && importantOnly == true && positionIsLower == true) {
							updateAllBehaviours(player, FEMALE_CROTCH_WILL_BE_VISIBLE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
						} else {
							updateAllBehaviours(player, FEMALE_HUMAN_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);	
							}	
						}
					}
			    }
		} else { 
				if (players[player].gender == eGender.MALE) {
					updateAllBehaviours(player, MALE_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
				} else {
					updateAllBehaviours(player, FEMALE_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
				}
																		
			var trigger = determineStrippingSituation(player);
			updateBehaviour(player, trigger, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], null);
		}
	} else {
		/* the player has no clothes and will have to accept a forfeit */
		if (players[player].gender == eGender.MALE) {
			updateAllBehaviours(player, MALE_MUST_MASTURBATE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
		} else if (players[player].gender == eGender.FEMALE) {
			updateAllBehaviours(player, FEMALE_MUST_MASTURBATE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
		}
		if (player != HUMAN_PLAYER) {
			var trigger = determineForfeitSituation(player);
			updateBehaviour(player, trigger, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], null);
		}
	}
	
	return clothes;
}

/************************************************************
 * Manages the dialogue triggers as player begins to strip
 * or forfeit.
 ************************************************************/
function prepareToStripPlayer (player) {
    /* count the clothing the player has remaining */
    var clothes = 0;
    for (var i = 0; i < players[player].clothing.length; i++) {
        if (players[player] && players[player].clothing[i]) {
            clothes++;
        }
    }
    var startingClothes = players[player].clothing.length;
    
	/* determine the situation */
	if (clothes > 0) {
		/* the player has clothes left and will strip */
        if (player == HUMAN_PLAYER) {
            if (players[HUMAN_PLAYER].gender == eGender.MALE) {
                updateAllBehaviours(player, MALE_HUMAN_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
            } else {
                updateAllBehaviours(player, FEMALE_HUMAN_MUST_STRIP, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
            }
        } else {
            var toBeRemovedClothing = players[player].clothing[startingClothes - 1];
            var dialogueTrigger = getClothingTrigger(player, toBeRemovedClothing, false);
            
            /* set up the replaceable tags and content */
            var replace = [NAME, PROPER_CLOTHING, LOWERCASE_CLOTHING, PLAYER_NAME];
            var content = [players[player].label, toBeRemovedClothing.proper, toBeRemovedClothing.lower, players[HUMAN_PLAYER].label];
        
            updateAllBehaviours(player, dialogueTrigger, replace, content, players[player]);
            updateBehaviour(player, PLAYER_STRIPPING, replace, content, null);
        }
	} else {
		/* the player has no clothes and will have to accept a forfeit */
		if (players[player].gender == eGender.MALE) {
			updateAllBehaviours(player, MALE_MUST_MASTURBATE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
		} else if (players[player].gender == eGender.FEMALE) {
			updateAllBehaviours(player, FEMALE_MUST_MASTURBATE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
		}

		if (player != HUMAN_PLAYER) {
			updateBehaviour(player, PLAYER_MUST_MASTURBATE, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], null);
		}
	}
}

/************************************************************
 * Sets up and displays the stripping modal, so that the human
 * player can select an article of clothing to remove.
 ************************************************************/
function showStrippingModal () {
    console.log("The stripping modal is being set up.");
    
    /* clear the area */
    $stripClothing.html("");
    selectedClothing = -1;
    
    /* load the current layer of clothing into the modal */
    var currentClothingID = 0;
    for (var i = 0; i < players[HUMAN_PLAYER].clothing.length; i++) {
        if (players[HUMAN_PLAYER].clothing[i]) {
            var clothingCard = 
                "<div class='clothing-modal-container'><input type='image' id='"+currentClothingID+"' value='"+i+"' class='bordered modal-clothing-image' src="+
                players[HUMAN_PLAYER].clothing[i].image+" onclick='selectClothingToStrip("+currentClothingID+")'/></div>";

            $stripClothing.append(clothingCard);
            currentClothingID += 1;
        }
    }
    
    /* disable the strip button */
    $stripButton.attr('disabled', true);
    
    /* display the stripping modal */
    $stripModal.modal('show');
    
    /* hijack keybindings */
    KEYBINDINGS_ENABLED = true;
    document.removeEventListener('keyup', game_keyUp, false);
    document.addEventListener('keyup', clothing_keyUp, false);
}

/************************************************************
 * The human player clicked on an article of clothing in
 * the stripping modal.
 ************************************************************/
function selectClothingToStrip (id) {
    console.log(Number($("#"+id+".modal-clothing-image").prop("value")));
    
    /* save the selected article */
    selectedClothing = Number($("#"+id+".modal-clothing-image").prop("value"));
    
    /* designate the selected article */
    $(".modal-selected-clothing-image").removeClass("modal-selected-clothing-image");
    $("#"+id+".modal-clothing-image").addClass("modal-selected-clothing-image");
    
    /* enable the strip button */
    $stripButton.attr('disabled', false);
}

/************************************************************
 * A keybound handler.
 ************************************************************/
function clothing_keyUp(e) 
{
    if (KEYBINDINGS_ENABLED) {
        if (e.keyCode == 32 && !$stripButton.prop('disabled')) { // Space
            closeStrippingModal();
        }
        else if (e.keyCode >= 49 && e.keyCode <= 57) { // A number key
            selectClothingToStrip(e.keyCode - 49);
        }
    }
}
 
/************************************************************
 * The human player closed the stripping modal. Removes an 
 * article of clothing from the human player. 
 ************************************************************/
 
function closeStrippingModal () {
    if (selectedClothing >= 0) {
        /* return keybindings */
        KEYBINDINGS_ENABLED = true;
        document.removeEventListener('keyup', clothing_keyUp, false);
        document.addEventListener('keyup', game_keyUp, false);
        
        /* grab the removed article of clothing */
        var removedClothing = players[HUMAN_PLAYER].clothing[selectedClothing];

        players[HUMAN_PLAYER].clothing[selectedClothing] = null;
	players[HUMAN_PLAYER].timeInStage = -1;
        
        /* figure out if it should be important */
        if (removedClothing.position != OTHER_ARTICLE) {
			var otherClothing;
            for (var i = 0; i < players[HUMAN_PLAYER].clothing.length; i++) {
                if (players[HUMAN_PLAYER].clothing[i] !== null
					&& players[HUMAN_PLAYER].clothing[i].position === removedClothing.position
					&& players[HUMAN_PLAYER].clothing[i].type != MINOR_ARTICLE) {
                    console.log(players[HUMAN_PLAYER].clothing[i]);
					otherClothing = players[HUMAN_PLAYER].clothing[i];
                    break;
                }
            }
            console.log(otherClothing);
            if (!otherClothing) {
                removedClothing.type = IMPORTANT_ARTICLE;
                players[HUMAN_PLAYER].exposed = true;
            } else if (removedClothing.type == IMPORTANT_ARTICLE) {
                removedClothing.type = MAJOR_ARTICLE;
				/* Just make any other remaining article important instead,
				   so that, if it is the last one, it's considered as such by
				   playerMustStrip() */
				otherClothing.type = IMPORTANT_ARTICLE;
            }
        }
        
        /* determine its dialogue trigger */
        var dialogueTrigger = getClothingTrigger(HUMAN_PLAYER, removedClothing, true);
        console.log(removedClothing);
        /* display the remaining clothing */
        displayHumanPlayerClothing();
        
        /* count the clothing the player has remaining */
        var clothes = 0;
        for (var i = 0; i < players[HUMAN_PLAYER].clothing.length; i++) {
            if (players[HUMAN_PLAYER].clothing[i]) {
                clothes++;
            }
        }
        var startingClothes = players[HUMAN_PLAYER].clothing.length;
        
        /* update label */
        if (clothes > 0) {
            $gameClothingLabel.html("Your Remaining Clothing");
        } else {
            $gameClothingLabel.html("You're Naked");
        }
            
        /* set up the replaceable tags and content */
        var replace = [NAME, PROPER_CLOTHING, LOWERCASE_CLOTHING, PLAYER_NAME];
        var content = [players[HUMAN_PLAYER].label, removedClothing.proper, removedClothing.lower, players[HUMAN_PLAYER].label];
        
        /* update behaviour */
        updateAllBehaviours(HUMAN_PLAYER, dialogueTrigger, replace, content, players[HUMAN_PLAYER]);
        updateAllGameVisuals();
		
		/* allow progression */
        $('#stripping-modal').modal('hide');
		endRound();
		allowProgression();
    } else {
        /* how the hell did this happen? */
        console.log("Error: there was no selected article.");
        showStrippingModal();
    }
}

/************************************************************
 * Removes an article of clothing from an AI player. Also 
 * handles all of the dialogue triggers involved in the process.
 ************************************************************/
function stripAIPlayer (player) {
	console.log("Opponent "+player+" is being stripped.");
	
	/* grab the removed article of clothing and determine its dialogue trigger */
	var removedClothing = players[player].clothing.pop();
	if (removedClothing.type === IMPORTANT_ARTICLE) {
		players[player].exposed = true;
	}
    players[player].clothing.unshift(null);
	var dialogueTrigger = getClothingTrigger(player, removedClothing, true);
	
	/* determine new AI stage */
    var clothes = countClothes(player);
    var startingClothes = players[player].clothing.length;
	
	players[player].stage = startingClothes - clothes;
	players[player].timeInStage = -1;
	
	/* set up the replaceable tags and content */
	var replace = [NAME, PROPER_CLOTHING, LOWERCASE_CLOTHING, PLAYER_NAME];
	var content = [players[player].label, removedClothing.proper, removedClothing.lower, players[HUMAN_PLAYER].label];
	
	/* update behaviour */
	updateAllBehaviours(player, dialogueTrigger, replace, content, players[player]);
	updateBehaviour(player, PLAYER_STRIPPED, replace, content, null);
    updateAllGameVisuals();
	
}

/************************************************************
 * Determines whether or not the provided player is winning
 * or losing at the end and returns the appropriate dialogue trigger.
 ************************************************************/
function determineForfeitSituation (player) {
	/* check to see how many players are out */
	for (var i = 0; i < players.length; i++) {
            if (players[i] && players[i].out) {
                if (players[i].out) {
		    return PLAYER_MUST_MASTURBATE;
                }
            }
	}
    return PLAYER_MUST_MASTURBATE_FIRST;
}

function countClothes (player) {
    var clothes = 0;
    for (var i = 0; i < players[player].clothing.length; i++) {
        if (players[player].clothing[i]) {
            clothes++;
        }
    }
    return clothes;
}

/************************************************************
 * Removes an article of clothing from the selected player.
 * Also handles all of the dialogue triggers involved in the 
 * process.
 ************************************************************/ 
function stripPlayer (player) {
    /* count the clothing the player has remaining */
    var clothes = countClothes(player);

	/* determine the situation */
	if (clothes > 0) {
		/* the player has clothes left and will strip */
		if (player == HUMAN_PLAYER) {
			showStrippingModal();
		} else {
			stripAIPlayer(player);
			/* allow progression */
			endRound();
		}
	} else {
		/* the player has no clothes and will have to accept a forfeit */
		players[player].forfeit = [PLAYER_MASTURBATING, CAN_SPEAK];
		players[player].out = true;
        
        if (chosenDebug === player) {
            chosenDebug = -1;
            updateDebugState(showDebug);
        }
		
		/* update behaviour */
		if (player == HUMAN_PLAYER) {
			if (players[HUMAN_PLAYER].gender == eGender.MALE) {
				updateAllBehaviours(HUMAN_PLAYER, MALE_START_MASTURBATING, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[HUMAN_PLAYER]);
			} else if (players[HUMAN_PLAYER].gender == eGender.FEMALE) {
				updateAllBehaviours(HUMAN_PLAYER, FEMALE_START_MASTURBATING, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[HUMAN_PLAYER]);
			}
			$gameClothingLabel.html("You're Masturbating...");
            $gamePlayerCountdown.show();
			setForfeitTimer(player);
		} else {
			if (players[player].gender == eGender.MALE) {
				updateAllBehaviours(player, MALE_START_MASTURBATING, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
			} else if (players[player].gender == eGender.FEMALE) {
				updateAllBehaviours(player, FEMALE_START_MASTURBATING,[NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], players[player]);
			}
			updateBehaviour(player, PLAYER_START_MASTURBATING, [NAME, PLAYER_NAME], [players[player].label, players[HUMAN_PLAYER].label], null);
			setForfeitTimer(player);
		}
		
		/* allow progression */
		endRound();
	}
}

/************************************************************
 * Counts the number of players in a certain stage
 ************************************************************/ 
function getNumPlayersInStage(stage) {
	var count = 0;
	for (var i = 0; i < players.length; i++) {
		if (!players[i]) { continue; }
		switch (stage) {
			case STAGE_ALIVE:
				if (!players[i].out) { count++; }
				break;
			case STAGE_NAKED:
				var clothes = 0;
    				for (var j = 0; j < players[i].clothing.length; j++) {
					if (players[i].clothing[j]) {
						clothes++;
					}
				}
				if (clothes === 0) { count++; }
				break;
			case STAGE_MASTURBATING:
				if (players[i].out && !players[i].finished) { count++; }
				break;
			case STAGE_FINISHED:
				if (players[i].finished) { count++; }
				break;
		}
	}
	return count;
}
