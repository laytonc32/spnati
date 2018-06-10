/********************************************************************************
 This file contains the variables and functions that form the poker mechanics and
 store information on the current poker state of each player. Anything regarding
 cards, except AI decisions, is part of this file (even UI).
 ********************************************************************************/
 
/**********************************************************************
 *****               Poker Hand Object Specification              *****
 **********************************************************************/
 
/* suit names */
var SPADES   = "spade";
var HEARTS   = "heart";
var CLUBS    = "clubs";
var DIAMONDS = "diamo";

/* hand strengths */
var NONE			= 0;
var HIGH_CARD 		= 1;
var PAIR			= 2;
var TWO_PAIR		= 3;
var THREE_OF_A_KIND	= 4;
var STRAIGHT		= 5;
var FLUSH			= 6;
var FULL_HOUSE		= 7;
var FOUR_OF_A_KIND	= 8;
var STRAIGHT_FLUSH	= 9;
var ROYAL_FLUSH 	= 10;

/************************************************************
 * Stores information on a poker hand.
 ************************************************************/
function createNewHand (cards, strength, value, tradeIns) {
	var newHandObject = {cards:cards, 
						 strength:strength, 
						 value:value, 
						 tradeIns:tradeIns};
						  
	return newHandObject;
}
 
/**********************************************************************
 *****                      Poker UI Elements                     *****
 **********************************************************************/

/* hidden cards and hidden card areas */
$gameHiddenArea = $('#game-hidden-area');
$hiddenLargeCard = $('#hidden-large-card');
 
/* player card cells */
$cardCells = [[$("#player-0-card-1"), $("#player-0-card-2"), $("#player-0-card-3"), $("#player-0-card-4"), $("#player-0-card-5")],
			     [$("#player-1-card-1"), $("#player-1-card-2"), $("#player-1-card-3"), $("#player-1-card-4"), $("#player-1-card-5")],
			     [$("#player-2-card-1"), $("#player-2-card-2"), $("#player-2-card-3"), $("#player-2-card-4"), $("#player-2-card-5")],
			     [$("#player-3-card-1"), $("#player-3-card-2"), $("#player-3-card-3"), $("#player-3-card-4"), $("#player-3-card-5")],
			     [$("#player-4-card-1"), $("#player-4-card-2"), $("#player-4-card-3"), $("#player-4-card-4"), $("#player-4-card-5")]];	

/**********************************************************************
 *****                       Poker Variables                      *****
 **********************************************************************/

/* pseudo constants */
var ANIM_DELAY = 350;
var ANIM_TIME = 1000;
var CARDS_PER_HAND = 5;
 
/* image constants */
var BLANK_CARD_IMAGE = IMG + "blankcard.jpg";
var UNKNOWN_CARD_IMAGE = IMG + "unknown.jpg";
 
/* card decks */
var inDeck = [];	/* cards left in the deck */
var outDeck = [];	/* cards waiting to be shuffled into the deck */

/* player hands */
var hands = [null, null, null, null, null];

/* deal lock */
var dealLock = 0;
 
/**********************************************************************
 *****                    Start Up Functions                      *****
 **********************************************************************/

/************************************************************
 * Sets up all of the information needed to start playing poker.
 ************************************************************/
function setupPoker () {
    /* set up the player hands */
    for (var i = 0; i < hands.length; i++) {
        hands[i] = createNewHand([null, null, null, null, null], NONE, 0, [false, false, false, false, false]);
    }
    
    /* compose a new deck */
    composeDeck();
}
 
/************************************************************
 * Composes a brand new deck of cards.
 ************************************************************/
function composeDeck () {
	inDeck = [];
    outDeck = [];
	var suit = "";
	
	for (var i = 0; i < 4; i++) {
		switch (i) {
			case 0: suit = SPADES;   break;
			case 1: suit = HEARTS;   break;
			case 2: suit = CLUBS;    break;
			case 3: suit = DIAMONDS; break;
		}
		
		for (j = 1; j < 14; j++) {
			inDeck.push(suit + j);
		}
	}
}

/**********************************************************************
 *****                      Card Functions                        *****
 **********************************************************************/
 
/************************************************************
 * Returns the numeric value of the card.
 ************************************************************/
function getCardValue (card) {
	return Number(card.substring(5));
}

/************************************************************
 * Returns the string suit of the card.
 ************************************************************/
function getCardSuit (card) {
	return card.substring(0, 5);
}

/************************************************************
 * Returns the numeric suit of the card.
 ************************************************************/
function getCardSuitValue (card) {
	var suit = card.substring(0, 5);
	
	if (suit == SPADES) {
		return 0;
	} else if (suit == HEARTS) {
		return 1;
	} else if (suit == CLUBS) {
		return 2;
	} else if (suit == DIAMONDS) {
		return 3;
	} else {
		return 4;
	}
}

/**********************************************************************
 *****                       UI Functions                         *****
 **********************************************************************/
 
/************************************************************
 * Sets the given card to full opacity.
 ************************************************************/
function fillCard (player, card) {
	$cardCells[player][card].css({opacity: 1});
}

/************************************************************
 * Sets the given card to a lower opacity.
 ************************************************************/
function dullCard (player, card) {
	$cardCells[player][card].css({opacity: 0.4});
}

/************************************************************
 * Shows the given player's hand at full opacity.
 ************************************************************/
function showHand (player) {
	for (var i = 0; i < hands[player].cards.length; i++) {
		$cardCells[player][i].attr('src', IMG + hands[player].cards[i] + ".jpg");
		fillCard(player, i);
	}
}

/************************************************************
 * Shows but completely dulls the given player's hand.
 ************************************************************/
function dullHand (player) {
	for (var i = 0; i < hands[player].cards.length; i++) {
		$cardCells[player][i].attr('src', IMG + hands[player].cards[i] + ".jpg");
		dullCard(player, i);
	}
}

/************************************************************
 * Hides the given player's hand based on their state.
 ************************************************************/
function hideHand (player) {
	for (var i = 0; i < hands[player].cards.length; i++) {
        if (players[player]) {
            if (!players[player].out) {
                $cardCells[player][i].attr('src', UNKNOWN_CARD_IMAGE);
            } else {
                $cardCells[player][i].attr('src', BLANK_CARD_IMAGE);
            }
            fillCard(player, i);
        }
	}
}

/************************************************************
 * Clears the given player's hand (in preparation of a new game).
 ************************************************************/
function clearHand (player) {
	if (hands[player] && hands[player].cards) {
		for (var i = 0; i < hands[player].cards.length; i++) {
			$cardCells[player][i].attr('src', BLANK_CARD_IMAGE);
			fillCard(player, i);
		}
	}
}

/*************************************************************
 * Stops all card animations.
 *************************************************************/
function stopCardAnimations () {
    $('.shown-card').stop(true, true);
    for (var p = 0; p < players.length; p++) {
        if (hands[p]) {
            for (var i = 0; i < hands[p].cards.length; i++) {
                if (hands[p].cards[i]) {
                    clearTimeout(hands[p].cards[i].timeoutID);
                }
            }
        }
    }
}


/**********************************************************************
 *****                      Card Functions                        *****
 **********************************************************************/

/************************************************************
 * Collects the given player's hand into the outDeck.
 ************************************************************/
function collectPlayerHand (player) {
	/* collect cards from the hand into the outDeck */
	for (var i = 0; i < hands[player].cards.length; i++) {
		if (hands[player].cards[i]) {
			outDeck.push(hands[player].cards[i]);
		}
		hands[player].cards[i] = null;
		$cardCells[player][i].attr('src', BLANK_CARD_IMAGE);
	}
}

/************************************************************
 * Shuffles the outDeck into the inDeck.
 ************************************************************/
function shuffleDeck () {
	/* shuffle the cards from the outDeck into the inDeck */
	for (var i = 0; i < outDeck.length; i++) {
        inDeck.push(outDeck[i]);
	}
	
	/* empty the outDeck */
	outDeck = [];
}

/************************************************************
 * Deals new cards to the given player.
 ************************************************************/
function dealHand (player) {
	/* collect their old hand */
	collectPlayerHand (player);
	
	/* first make sure the deck has enough cards */
	if (inDeck.length < hands[player].cards.length) {
		shuffleDeck();
	}
	
	/* deal the new cards */
	var drawnCard;
	for (var i = 0; i < hands[player].cards.length; i++) {
		drawnCard = getRandomNumber(0, inDeck.length);
        $cardCells[player][i].attr('src', BLANK_CARD_IMAGE);
		hands[player].cards[i] = inDeck[drawnCard];
		inDeck.splice(drawnCard, 1);
		delayDealtCard(player, i);
	}
}

/************************************************************
 * Exchanges the chosen cards for the given player.
 ************************************************************/
function exchangeCards (player) {
	/* determine how many cards are being swapped */
	var swap = 0;
	for (var i = 0; i < hands[player].cards.length; i++) {
		if (hands[player].tradeIns[i]) {
			swap++;
		}
	}
	
	/* make sure the deck has enough cards */
	if (inDeck.length < swap) {
		shuffleDeck();
	}
	
	/* collect their old cards */
	for (var i = 0; i < hands[player].cards.length; i++) {
		if (hands[player].tradeIns[i] && hands[player].cards[i]) {
			outDeck.push(hands[player].cards[i]);
			hands[player].cards[i] = null;
		}
	}
	
	/* take the new cards */
	var drawnCard;
	for (var i = 0; i < hands[player].cards.length; i++) {
		if (hands[player].tradeIns[i]) {
			drawnCard = getRandomNumber(0, inDeck.length);
			hands[player].cards[i] = inDeck[drawnCard];
			inDeck.splice(drawnCard, 1);
            hands[player].tradeIns[i] = false;
		}
	}
}

/**********************************************************************
 *****                    Animation Functions                     *****
 **********************************************************************/

/************************************************************
 * Adds a short delay to the dealt card animation.
 ************************************************************/
function delayDealtCard (player, card) {
	card.timeoutID = window.setTimeout(function(){animateDealtCard(player, card)}, (player*(ANIM_DELAY/5)) + (card*ANIM_DELAY));
}

/************************************************************
 * Animates a small card into a player's hand.
 ************************************************************/
function animateDealtCard (player, card) {
	card.timeoutID = undefined;
	$clonedCard = $hiddenLargeCard.clone().prependTo($gameHiddenArea);
	$clonedCard.addClass("shown-card");
	$clonedCard.attr('id', 'dealt-card-'+player+'-'+card);
	
	if (player == HUMAN_PLAYER) {
      $clonedCard.addClass("large-dealt-card");
	} else {
      $clonedCard.addClass("small-dealt-card");
	}
	
	var offset = $cardCells[player][card].offset();
	var top = offset.top - $gameHiddenArea.offset().top;
	var left = offset.left - $gameHiddenArea.offset().left - 6;

	// Skip animation time calculation if skipping animation
	if (ANIM_TIME === 0) {
		var animTime = 0;
	} else {
		// Set card speed according to desired time to deal card to farthest position
		var speed = getFarthestDealDistance() / ANIM_TIME;
		var distance = offsetDistance($clonedCard.offset(), {left: left, top: top});
		var animTime = distance / speed;
	}

	$clonedCard.animate({top:top, left:left}, animTime, function() {
		$('#dealt-card-'+player+'-'+card).remove();
		$cardCells[player][card].attr('src', UNKNOWN_CARD_IMAGE);
		dealLock++;
	});
}

/************************************************************
 * Get the farthest distance between any player card and the dealer
 ************************************************************/
function getFarthestDealDistance()
{
	return offsetDistance($('#player-4-card-5').offset(), $('#hidden-large-card').offset());
}

/************************************************************
 * Get the distance between 2 offsets (objects with top & left)
 ************************************************************/
function offsetDistance (offset1, offset2)
{
	return distance2d(offset1.left, offset1.top, offset2.left, offset2.top);
}


/************************************************************
 * Pythagorean theorem for 2 dimensions
 ************************************************************/
function distance2d (x1, y1, x2, y2)
{
	return Math.sqrt(Math.pow(x1 - x2, 2) + Math.pow(y1 - y2, 2));
}

/**********************************************************************
 *****                      Poker Functions                       *****
 **********************************************************************/

/************************************************************
 * Maps the hand strength to a string.
 ************************************************************/
function handStrengthToString (number) {
	switch (number) {
		case NONE: 				return "Nothing";
		case HIGH_CARD: 		return "High Card";
		case PAIR: 				return "One Pair";
		case TWO_PAIR: 			return "Two Pair";
		case THREE_OF_A_KIND: 	return "Three of a Kind";
		case STRAIGHT: 			return "Straight";
		case FLUSH: 			return "Flush";
		case FULL_HOUSE: 		return "Full House";
		case FOUR_OF_A_KIND:	return "Four of a Kind";
		case STRAIGHT_FLUSH: 	return "Straight Flush";
		case ROYAL_FLUSH: 		return "Royal Flush";
	}
}
 
/************************************************************
 * Returns the player number with the lowest hand.
 ************************************************************/
function determineLowestHand () {
	var lowestStrength = 11;
	var lowestPlayers = [];
	console.log();
    
	for (i = 0; i < players.length; i++) {
		if (players[i] && !players[i].out) {
			if (hands[i].strength < lowestStrength) {
				lowestStrength = hands[i].strength;
				lowestPlayers = [i];
			} else if (hands[i].strength == lowestStrength) {
				lowestPlayers.push(i);
			}
		}
	}
    console.log("Start of lowest hand determination, currently: "+lowestPlayers);
	
	if (lowestPlayers.length == 1) {
		return lowestPlayers[0];
	}

	/* need to break a tie */
	var maxTieBreakers = hands[lowestPlayers[0]].value.length;

	for (var currentTieBreaker = 0; currentTieBreaker < maxTieBreakers; currentTieBreaker++) {
		var lowestValue = 15;
		var tiedPlayers = lowestPlayers;
		console.log("Players Tied: "+tiedPlayers);

		for (var i = 0; i < tiedPlayers.length; i++) {
			if (hands[tiedPlayers[i]].value[currentTieBreaker] < lowestValue) {
				lowestValue = hands[tiedPlayers[i]].value[currentTieBreaker];
				console.log("Player "+tiedPlayers[i]+" is the new lowest with: "+hands[tiedPlayers[i]].value[currentTieBreaker]);
				lowestPlayers = [tiedPlayers[i]];
			} else if (hands[tiedPlayers[i]].value[currentTieBreaker] == lowestValue) {
				lowestPlayers.push(tiedPlayers[i]);
				console.log("Player "+tiedPlayers[i]+" is tied with: "+hands[tiedPlayers[i]].value[currentTieBreaker]);
			}
		}

		if (lowestPlayers.length == 1) {
			return lowestPlayers[0];
		}
	}

	/* unresolvable tie */
	return -1;
}
 
/************************************************************
 * Determine value of a given player's hand.
 ************************************************************/
function determineHand (player) {
	/* start by getting a shorthand variable and resetting */
	var hand = hands[player].cards;
	hands[player].strength = NONE;
	hands[player].value = 0;
	
	/* look for each strength, in composition */
	var have_pair = [];
	var have_three_kind = 0;
	var have_straight = 0;
	var have_flush = 0;

	/* start by collecting the ranks and suits of the cards */
	var cardRanks = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
	var cardSuits = [0, 0, 0, 0];
	
	for (var i = 0; i < hand.length; i++) {
		cardRanks[getCardValue(hand[i]) - 1]++;
		if (getCardValue(hand[i]) == 1) {
			cardRanks[13]++;
		}
		cardSuits[getCardSuitValue(hand[i])]++;
	}
	
	/* look for four of a kind, three of a kind, and pairs */
	for (var i = cardRanks.length-1; i > 0; i--) {
		if (cardRanks[i] == 4) {
			hands[player].strength = FOUR_OF_A_KIND;
			hands[player].value = [i+1];
			break;
		} else if (cardRanks[i] == 3) {
			have_three_kind = i+1;
		} else if (cardRanks[i] == 2) {
			have_pair.push(i+1);
		}
	}
	
	/* determine full house, three of a kind, two pair, and pair */
	if (hands[player].strength == NONE) {
		if (have_three_kind && have_pair.length > 0) {
			hands[player].strength = FULL_HOUSE;
			hands[player].value = [have_three_kind];
		} else if (have_three_kind) {
			hands[player].strength = THREE_OF_A_KIND;
			hands[player].value = [have_three_kind];
		} else if (have_pair.length > 0) {
			hands[player].strength = have_pair.length == 2 ? TWO_PAIR : PAIR;
			hands[player].value = have_pair;
			
			for (var i = cardRanks.length-1; i > 0; i--) {
				if (cardRanks[i] == 1) {
					hands[player].value.push(i+1);
				}
			}
		}
	}
	
	/* look for straights and flushes */
	if (hands[player].strength == NONE) {
		/* first, straights */
		var sequence = 0;

		for (var i = 0; i < cardRanks.length; i++) {
			if (cardRanks[i] == 1) {
				sequence++;
				if (sequence == hand.length) {
					/* one card each of five consecutive ranks is a
					 * straight */
					have_straight = i+1;
					break;
				}
			} else if (sequence > 0) {
                if (i == 1) {
					/* Ace but no deuce is OK - we might have 10-A */
                    sequence = 0;
                } else {
                    /* A hole in the sequence - can't have a straight */
                    break;
                }
			}
		}
		
		/* second, flushes */
		for (var i = 0; i < cardSuits.length; i++) {
			if (cardSuits[i] == hand.length) {
				/* this is a flush */
				have_flush = 1;
				break;
			} else if (cardSuits[i] > 0) {
				/* can't have a flush */
				break;
			}
		}
		
		/* determine royal flush, straight flush, flush, straight, and high card */
		if (have_flush && have_straight == 14) {
			hands[player].strength = ROYAL_FLUSH;
			hands[player].value = [have_straight];
		} else if (have_flush && have_straight) {
			hands[player].strength = STRAIGHT_FLUSH;
			hands[player].value = [have_straight];
		} else if (have_straight) {
			hands[player].strength = STRAIGHT;
			hands[player].value = [have_straight];
		} else {
			hands[player].strength = (have_flush ? FLUSH : HIGH_CARD);
			hands[player].value = [];
			
			for (var i = cardRanks.length-1; i > 0; i--) {
				if (cardRanks[i] == 1) {
					hands[player].value.push(i+1);
				}
			}
		}
	}

	/* stats for the log */
    console.log();
	console.log("Player "+player+" Hand Analysis");
	console.log("Rank: "+cardRanks);
	console.log("Suit: "+cardSuits);
	console.log("Player has " +handStrengthToString(hands[player].strength)+" of value "+hands[player].value);
}
