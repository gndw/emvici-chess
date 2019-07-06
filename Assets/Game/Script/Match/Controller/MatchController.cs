using System;
using System.Collections.Generic;
using Agate.Chess.Board.Controller;
using Agate.Chess.Board.Utility;
using Agate.Chess.Chessman.Utility;
using Agate.Chess.Prefab.Controller;
using Agate.MVC.Core;
namespace Agate.Chess.Match.Controller
{
    public class MatchController : SceneController
    {
        private enum MatchTurnState
        {
            Idle,
            ChessmanSelected,
            Moving
        }
        private class MatchState
        {
            public ChessmanColorType Player;
            public MatchTurnState TurnState;
            public BoardCoord SelectedCoord;
            public List<BoardCoord> PossibleMoves;
            public MatchState(ChessmanColorType playerType)
            {
                Player = playerType;
                Reset();
            }
            public void Reset()
            {
                TurnState = MatchTurnState.Idle;
                SelectedCoord = null;
                PossibleMoves = new List<BoardCoord>();
            }
        }

        [Inject]
        private PrefabController _prefabController
        {
            get;
            set;
        }
        private MatchState _currentState;
        private BoardController _boardController = new BoardController();

        protected override void Load(Sequence createLoadingSequence)
        {
            createLoadingSequence.AddSequence((next) =>
            {
                _boardController.Init(_prefabController);
                next();
            });
        }
        public override void StartScene()
        {
            StartMatch();
        }
        private void StartMatch()
        {
            _currentState = new MatchState(ChessmanColorType.Light);
            _boardController.OnChessmanSelected += OnChessmanSelected;
            _boardController.OnBoardSelected += OnBoardSelected;
        }
        private void SwitchTurn()
        {
            _boardController.ClearHighlight();
            _currentState.Reset();
            switch (_currentState.Player)
            {
                case ChessmanColorType.Light:
                    _currentState.Player = ChessmanColorType.Dark;
                    break;
                case ChessmanColorType.Dark:
                    _currentState.Player = ChessmanColorType.Light;
                    break;
            }
        }
        private void SelectChessman(BoardCoord coord)
        {
            List<BoardCoord> possibleMoves = _boardController.GetPossibleMoves(coord);
            if (possibleMoves.Count > 0)
            {
                _boardController.SetHighlight(possibleMoves);
                _currentState.PossibleMoves = possibleMoves;
                _currentState.SelectedCoord = coord;
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
            _currentState.Reset();
        }
        private void Move(BoardCoord originCoord, BoardCoord targetCoord, Action onFinish)
        {
            _currentState.TurnState = MatchTurnState.Moving;
            _boardController.Move(originCoord, targetCoord, SwitchTurn);
        }
        private void OnBoardSelected(BoardCoord coord)
        {
            switch (_currentState.TurnState)
            {
                case MatchTurnState.ChessmanSelected:
                    {
                        if (_currentState.PossibleMoves.Contains(coord)) Move(_currentState.SelectedCoord, coord, SwitchTurn);
                        else UnSelectChessman();
                        break;
                    }
            }
        }
        private void OnChessmanSelected(ChessmanType type, ChessmanColorType colorType, BoardCoord coord)
        {
            switch (_currentState.TurnState)
            {
                case MatchTurnState.Idle:
                    {
                        if (colorType == _currentState.Player)
                            SelectChessman(coord);
                        break;
                    }
                case MatchTurnState.ChessmanSelected:
                    {
                        if (colorType == _currentState.Player)
                            SelectChessman(coord);
                        else
                        {
                            if (_currentState.PossibleMoves.Contains(coord))
                                Move(_currentState.SelectedCoord, coord, SwitchTurn);
                        }
                        break;
                    }
            }
        }
    }
}