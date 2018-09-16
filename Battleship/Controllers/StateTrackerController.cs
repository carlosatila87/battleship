using Battleship.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Battleship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateTrackerController : Controller
    {
        private Board _board; 

        public StateTrackerController()
        {
            _board = Board.GetBoard();
        }

        [HttpPost("[action]")]
        public IActionResult CreateBoard()
        {
            try
            {
                _board = Board.NewBoard();                                
                return Ok();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            
        }
        
        [HttpPost("[action]")]
        public IActionResult AddBattleship(StateTrackerBattleship battleship)
        {
            try
            {
                //Check if Coordinate is valid
                if (!Coordinate.IsValid(battleship.Coordinate))
                    return BadRequest();
                var coordinate = new Coordinate(battleship.Coordinate);
                
                //Check if Orientation is HORIZONTAL or VERTICAL
                if (battleship.Orientation.ToUpper() != StateTrackerOrientation.Vertical.ToUpper() && battleship.Orientation.ToUpper() != StateTrackerOrientation.Horizontal.ToUpper())
                    return BadRequest();
                
                //Check if the Ship fits in the board from the indicated position and orientation and if it has a valid size
                if (battleship.Size < 1 || 
                    ((battleship.Size + Convert.ToInt32(coordinate.VerticalPoint) > 10) && battleship.Orientation.ToUpper() == StateTrackerOrientation.Vertical.ToUpper())  ||
                    ((battleship.Size + coordinate.HorizontalPoint > 10) && battleship.Orientation.ToUpper() == StateTrackerOrientation.Horizontal.ToUpper()))
                    return BadRequest();

                for (var i = 0; i < battleship.Size; i++)
                {
                    if (battleship.Orientation.ToUpper() == StateTrackerOrientation.Vertical.ToUpper())
                    {
                        _board.Points[Convert.ToInt32(coordinate.VerticalPoint) - 1 + i, coordinate.HorizontalPoint - 1].point = Point.State.Ship;
                    }
                    else
                    {
                        _board.Points[Convert.ToInt32(coordinate.VerticalPoint) - 1, coordinate.HorizontalPoint - 1 + i].point = Point.State.Ship;
                    }
                }
                return Ok();                
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }            
        }        

        [HttpPost("[action]")]
        public IActionResult Attack(StateTrackerCoordinate coord)
        {
            try
            {
                //Check if Coordinate is valid
                if (!Coordinate.IsValid(coord.Coordinate))
                    return BadRequest();
                var coordinate = new Coordinate(coord.Coordinate);

                //Check if the current position is empty
                if (_board.Points[Convert.ToInt32(coordinate.VerticalPoint) - 1, coordinate.HorizontalPoint - 1].point == Point.State.Empty)
                {
                    _board.Points[Convert.ToInt32(coordinate.VerticalPoint) - 1, coordinate.HorizontalPoint - 1].point = Point.State.Missed;
                    return Ok(new StateTrackerAttackReturn
                    {
                        LastAttack = coord.Coordinate,
                        State = StateTrackerMessages.YouMissed,
                        GameStatus = Board.GetGameStatus()
                    });
                }
                //Check if the current position has a ship
                else if (_board.Points[Convert.ToInt32(coordinate.VerticalPoint) - 1, coordinate.HorizontalPoint - 1].point == Point.State.Ship)
                {
                    _board.Points[Convert.ToInt32(coordinate.VerticalPoint) - 1, coordinate.HorizontalPoint - 1].point = Point.State.Bomb;
                    return Ok(new StateTrackerAttackReturn
                    {
                        LastAttack = coord.Coordinate,
                        State = StateTrackerMessages.YouHit,
                        GameStatus = Board.GetGameStatus()
                    });
                }
                //Repeated shot
                else
                {
                    return Ok(new StateTrackerAttackReturn
                    {
                        LastAttack = coord.Coordinate,
                        State = StateTrackerMessages.RepeatedShot,
                        GameStatus = Board.GetGameStatus()
                    });
                }
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }                                    
        }        
    }
}