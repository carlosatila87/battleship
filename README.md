# battleship
A WebAPI to implement a Battleship State-Tracker

The API can be consumed by POST requests after compiled.

# Create new board: (/api/statetracker/createboard)
Example:
http://localhost:57972/api/statetracker/createboard

# Add Battleship: (/api/statetracker/addbattleship)
Example:
http://localhost:57972/api/statetracker/addbattleship

Content </text> JSON:
{
	size: 3,
	coordinate: "A1",
	orientation: "VERTICAL"
}

# Attack: (/api/statetracker/attack)
Example:
http://localhost:57972/api/statetracker/attack

Content </text> JSON:
{
	coordinate: "C1"
}

Return:
{
	"lastAttack": "C1",
	"state": "You hit a ship",
	"gameStatus": "You Won!"
}
