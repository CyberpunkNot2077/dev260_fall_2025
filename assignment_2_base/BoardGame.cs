using System;
using System.Runtime.ExceptionServices;

namespace Week3ArraysSorting
{
    /// <summary>
    /// Board Game implementation for Assignment 2 Part A
    /// Demonstrates multi-dimensional arrays with interactive gameplay
    /// 
    /// Learning Focus: 
    /// - Multi-dimensional array manipulation (char[,])
    /// - Console rendering and user input
    /// - Game state management and win detection
    /// 
    /// Choose ONE game to implement:
    /// - Tic-Tac-Toe (3x3 grid)
    /// - Connect Four (6x7 grid with gravity)
    /// - Or something else creative using a 2D array! (I need to be able to understand the rules from your instructions)
    /// </summary>
    public class BoardGame
    {
        // Using Tic-Tac-Toe for this assignment
        private char[,] board = new char[3, 3];

        // Game state fields
        private char currentPlayer = 'X';
        private bool gameOver = false;
        private string winner = "";

        /// <summary>
        /// Constructor - Initialize the board game
        /// TODO: Set up your chosen game
        /// </summary>
        public BoardGame()
        {
            // TODO: Initialize your board array
            // For Tic-Tac-Toe or Connect Four, fill with empty spaces or dots
            // ❌ ⭕ -> use for Tic-Tac-Toe if you'd like for each square/player and the white box from array example

            ClearBoard();
            Console.WriteLine("Tic-Tac-Toe initialized. Let's-a-go!");
        }
        
        /// <summary>
        /// Main game loop - handles the complete game session
        /// TODO: Implement the full game experience
        /// </summary>
        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== TIC-TAC-TOE ===");
            Console.WriteLine();
            
            // TODO: Display game instructions
            DisplayInstructions();
            
            // TODO: Implement main game loop
            bool playAgain = true;
            
            while (playAgain)
            {
                // TODO: Reset game state for new game
                InitializeNewGame();
                
                // TODO: Play one complete game
                PlayOneGame();
                
                // TODO: Ask if player wants to play again
                playAgain = AskPlayAgain();
            }
            
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Display game instructions and controls
        /// TODO: Customize for your chosen game
        /// </summary>
        private void DisplayInstructions()
        {
            
            // Example for Tic-Tac-Toe:
            // Console.WriteLine("TIC-TAC-TOE RULES:");
            // Console.WriteLine("- Players take turns placing X and O");
            // Console.WriteLine("- Enter row and column (0-2) when prompted");
            // Console.WriteLine("- First to get 3 in a row wins!");
            
            // Example for Connect Four:
            // Console.WriteLine("CONNECT FOUR RULES:");
            // Console.WriteLine("- Players take turns dropping tokens");
            // Console.WriteLine("- Enter column number (0-6) when prompted");
            // Console.WriteLine("- First to get 4 in a row wins!");
            
            Console.WriteLine("Press any key to begin the game...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("=== Welcome to TIC-TAC-TOE! ===");
            Console.WriteLine("Here are the Rules:");
            Console.WriteLine("Players take turns putting X and O on the board.");
            Console.WriteLine("To make a move, enter the row and column numbers (0, 1, or 2) separated by a space.");
            Console.WriteLine("The first player to get 3 in a row (horizontally, vertically, or diagonally) is declared the winner.");
            Console.WriteLine("But if all 9 squares are full with no winner, the game will end in a tie.");
            Console.WriteLine("Good luck and have fun!");
        }
        
        /// <summary>
        /// Initialize/reset the game for a new round
        /// TODO: Reset board and game state
        /// </summary>
        private void InitializeNewGame()
        {
            // TODO: Clear the board array
            // TODO: Reset current player to 'X'
            // TODO: Reset game over flag
            // TODO: Clear winner
            
            ClearBoard();
            currentPlayer = 'X';
            gameOver = false;
            winner = "";
            Console.Clear();
            Console.WriteLine("Let the game begin! Player X goes first.");
            RenderBoard();
            
        }
        
        /// <summary>
        /// Play one complete game until win/draw/quit
        /// TODO: Implement the core game loop
        /// </summary>
        private void PlayOneGame()
        {
            // TODO: Game loop structure:
            // while (!gameOver)
            // {
            //     RenderBoard();
            //     GetPlayerMove();
            //     UpdateBoard();
            //     CheckWinCondition();
            //     SwitchPlayer();
            // }
            
            while (!gameOver){
                RenderBoard();
                GetPlayerMove();
                CheckWinCondition();
                if(!gameOver){
                    SwitchPlayer();
                }
            }

        }
        
        /// <summary>
        /// Render the current board state to console
        /// TODO: Create clear, readable board display
        /// </summary>
        private void RenderBoard()
        {
            // TODO: Display your multi-dimensional array as a visual board
            // Requirements:
            // - Clear, human-readable format
            // - Show current board state
            // - Include row/column labels for easy reference
            
            // Column headers
            Console.WriteLine("   0   1   2");

            for (int r = 0; r < 3; r++)
            {
                // Row with cell contents
                Console.Write(r + "  ");
                for (int c = 0; c < 3; c++)
                {
                    char val = board[r, c];
                    Console.Write(" " + (val == ' ' ? ' ' : val) + " ");
                    if (c < 2) Console.Write("|");
                }
                Console.WriteLine();

                // Separator between rows
                if (r < 2) Console.WriteLine("  ---+---+---");
            }
            Console.WriteLine();
        }
        
        /// <summary>
        /// Get and validate player move input
        /// TODO: Handle user input with validation
        /// </summary>
        private void GetPlayerMove()
        {
            // TODO: Prompt current player for their move
            // TODO: Validate input (in bounds, empty cell, etc.)
            // TODO: Keep asking until valid move is entered
            Console.WriteLine($"Player {currentPlayer}, please enter your move (row and column): ");
            bool validMove = false;
            while (!validMove){
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)){
                    Console.WriteLine("Invalid input. Please enter row and column separated by a space.");
                    continue;
                }

                string[] parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2){
                    Console.WriteLine("Invalid input. Please enter row and column separated by a space.");
                    continue;
                }
                if (!int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col)){
                    Console.WriteLine("Invalid input. Please enter numeric values for row and column.");
                    continue;
                }
                if (row < 0 || row > 2 || col < 0 || col > 2){
                    Console.WriteLine("Invalid input. Row and column must be between 0 and 2.");
                    continue;
                }
                if (board[row, col] != ' '){
                    Console.WriteLine("Invalid move. That cell is already taken.");
                    continue;
                }
                board[row, col] = currentPlayer;
                validMove = true;
            }
            
            
            // Example input validation structure:
            // bool validMove = false;
            // while (!validMove)
            // {
            //     Console.Write($"Player {currentPlayer}, enter your move: ");
            //     string input = Console.ReadLine();
            //     
            //     // Parse and validate input
            //     // Set validMove = true when valid move found
            // }
        }
        
        /// <summary>
        /// Check if current board state has a winner or draw
        /// TODO: Implement win detection logic
        /// </summary>
        private void CheckWinCondition()
        {
            // TODO: Check for win conditions specific to your game
            
            // For Tic-Tac-Toe:
            // - Check all rows, columns, and diagonals for 3 in a row
            // - Check for draw (board full, no winner)
            
            // For Connect Four:
            // - Check horizontal, vertical, and diagonal lines for 4 in a row
            // - Check for draw (top row full, no winner)
            
            winner = "";

            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                {
                    winner = currentPlayer.ToString();
                    gameOver = true;
                    Console.WriteLine($"The winner is Player {currentPlayer}!");
                    return;
                }
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                {
                    winner = currentPlayer.ToString();
                    gameOver = true;
                    Console.WriteLine($"The winner is Player {currentPlayer}!");
                    return;
                }
            }

            // Check diagonals
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
            {
                winner = currentPlayer.ToString();
                gameOver = true;
                Console.WriteLine($"The winner is Player {currentPlayer}!");
                return;
            }

            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
            {
                winner = currentPlayer.ToString();
                gameOver = true;
                Console.WriteLine($"The winner is Player {currentPlayer}!");
                return;
            }

            // Check for draw (board full and no winner)
            bool boardFull = true;
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == ' ')
                    {
                        boardFull = false;
                        break;
                    }
                }
                if (!boardFull) break;
            }

            if (boardFull)
            {
                gameOver = true;
                Console.WriteLine("Ladies and Gentlemen, we have a tie!");
                return;
            }
        }
        
        /// <summary>
        /// Ask player if they want to play another game
        /// TODO: Simple yes/no prompt with validation
        /// </summary>
        private bool AskPlayAgain()
        {
            // TODO: Ask user if they want to play again
            // TODO: Validate input (y/n, yes/no, etc.)
            // TODO: Return true for play again, false to return to main menu
            
            Console.WriteLine("Care to playe again? (y/n): ");
            string? input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)){
                input = input.ToLower();
                if (input == "y" || input == "yes"){
                    return true;
                } else if (input == "n" || input == "no"){
                    return false;
                }
                Console.WriteLine("That input is invalid. Please enter 'y' or 'n'.");
                return AskPlayAgain();
            }
            
            // Placeholder - always return false for now
            return false;
        }
        
        /// <summary>
        /// Switch to the next player's turn
        /// TODO: Toggle between X and O
        /// </summary>
        private void SwitchPlayer()
        {
            // TODO: Switch currentPlayer between 'X' and 'O'            
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }

        /// <summary>
        /// Clear or initialize the board array for a new game
        /// </summary>
        private void ClearBoard()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    board[r, c] = ' ';
                }
            }
        }
        
        // TODO: Add helper methods as needed
        // Examples:
        // - IsValidMove(int row, int col)
        // - IsBoardFull()
        // - CheckRow(int row, char player)
        // - CheckColumn(int col, char player)
        // - CheckDiagonals(char player)
        // - DropToken(int column, char player) // For Connect Four

        /// <summary>
        /// Returns true if the given row/col is a valid empty cell on the board
        /// </summary>
        private bool IsValidMove(int row, int col)
        {
            if (row < 0 || row > 2 || col < 0 || col > 2)
            {
                return false;
            }

            if (board[row, col] != ' ')
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true when there are no empty cells left on the board
        /// </summary>
        private bool IsBoardFull()
        {
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (board[r, c] == ' ')
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool CheckRow(int row, char player)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[row, c] != player)
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckColumn(int col, char player)
        {
            for (int r = 0; r < 3; r++)
            {
                if (board[r, col] != player){
                    return false;
                }
            }
            return true;
        }
        private bool CheckDiagonals(char player){
            if (board[0,0] == player && board[1,1] == player && board[2,2] == player){
                return true;
            }
            if (board[0,2] == player && board[1,1] == player && board[2,0] == player){
                return true;
            }
            return false;
        }
        //For Connect Four
        private void DropToken(int column, char player)
        {
            for (int r = 2; r >= 0; r--){
                if (board[r, column] == ' '){
                    board[r, column] = player;
                    break;
                }
            }
        }
    }
}
