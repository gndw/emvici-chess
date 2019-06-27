using System;
using System.Collections.Generic;
using Agate.Chess.Board.Controller;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Controller;
using Agate.Chess.Chessman.Utility;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Match.Controller
{
    public class MatchController : SceneController
    {
        [SerializeField]
        private BoardController _boardController = null;

        private enum MatchTurnState {Idle, ChessmanSelected, Moving}
        private class MatchState
        {
            public ChessmanColorType Player;
            public MatchTurnState TurnState = MatchTurnState.Idle;
            public IChessmanController SelectedChessman = null;
            public List<BoardCoord> PossibleMoves = new List<BoardCoord>();

            public MatchState(ChessmanColorType playerType)
            {
                Player = playerType;
            }
        }

        private MatchState _currentState;

        public override void Load()
        {
            _boardController.Init();

            StartMatch();
        }

        private void StartMatch()
        {
            _currentState = new MatchState(ChessmanColorType.Light);

            _boardController.OnChessmanControllerSelected += OnChessmanControllerSelected;
            _boardController.OnBoardSelected += OnBoardSelected;
        }

        private void SwitchTurn()
        {
            _boardController.ClearHighlight();
            _currentState.SelectedChessman = null;
            _currentState.PossibleMoves.Clear();
            _currentState.TurnState = MatchTurnState.Idle;
            switch (_currentState.Player)
            {
                case ChessmanColorType.Light: _currentState.Player = ChessmanColorType.Dark; break;
                case ChessmanColorType.Dark: _currentState.Player = ChessmanColorType.Light; break;
            }
        }

        private void SelectChessman(IChessmanController icc)
        {
            List<BoardCoord> possibleMoves = _boardController.GetPossibleMoves(icc);
            if (possibleMoves.Count > 0)
            {
                _boardController.SetHighlight(possibleMoves);
                _currentState.PossibleMoves = possibleMoves;
                _currentState.SelectedChessman = icc;
                _currentState.TurnState = MatchTurnState.ChessmanSelected;
            }
            else
            {
                UnSelectChessman();
            }
        }

        private void UnSelectChessman()
        {
            _boardController.ClearHighlight();
            _currentState.TurnState = MatchTurnState.Idle;
            _currentState.PossibleMoves.Clear();
            _currentState.SelectedChessman = null;
        }

        private void OnBoardSelected(BoardCoord coord)
        {
            switch (_currentState.TurnState)
            {
                case MatchTurnState.ChessmanSelected:
                {
                    if (_currentState.PossibleMoves.Contains(coord))
                    {
                        IChessmanController icc;
                        if (_boardController.TryGetChessman(coord, out icc))
                        {
                            _currentState.TurnState = MatchTurnState.Moving;
                            _boardController.Eat(_currentState.SelectedChessman, icc, SwitchTurn);
                        }
                        else
                        {
                            _currentState.TurnState = MatchTurnState.Moving;
                            _boardController.Move(_currentState.SelectedChessman, coord, SwitchTurn);
                        }
                    }
                    else
                    {
                        UnSelectChessman();
                    }
                    break;
                }
            }
        }

        private void OnChessmanControllerSelected(IChessmanController icc)
        {
            switch (_currentState.TurnState)
            {
                case MatchTurnState.Idle:
                {
                    if (icc.GetChessmanColorType() == _currentState.Player)
                    {
                        SelectChessman(icc);
                    }
                    break;
                }
                case MatchTurnState.ChessmanSelected:
                {
                    if (icc.GetChessmanColorType() == _currentState.Player)
                    {
                        SelectChessman(icc);
                    }
                    else
                    {
                        if (_currentState.PossibleMoves.Contains(icc.GetBoardCoord()))
                        {
                            _currentState.TurnState = MatchTurnState.Moving;
                            _boardController.Eat(_currentState.SelectedChessman, icc, SwitchTurn);
                        }
                    }
                    break;
                }
            }
        }
    }
}