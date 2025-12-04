using System.Collections;

namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Use switch statement on mode to select correct queue
            // Don't forget to call player.JoinQueue()!
            player.JoinQueue();
            switch (mode)
            {
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    break;
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    break;
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    break;
                default:
                    throw new ArgumentException($"Unknown game mode: {mode}");
            }
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Different logic needed for each mode
            // Remember to check queue count first!
            var queue = GetQueueByMode(mode);
            if (queue.Count < 2)
            {
                return null;
            }
            Player? player1 = null;
            Player? player2 = null;
            if (mode == GameMode.Casual)
            {
                // FIFO pairing for casual
                player1 = queue.Dequeue();
                player2 = queue.Dequeue();

                // Mark players as leaving the queue
                player1.LeaveQueue();
                player2.LeaveQueue();

                return new Match(player1, player2, mode);
            }
            else if (mode == GameMode.Ranked)
            {
                // Find the first pair within ±2 skill rating
                var tempList = queue.ToList();
                bool found = false;
                for (int i = 0; i < tempList.Count && !found; i++)
                {
                    for (int j = i + 1; j < tempList.Count && !found; j++)
                    {
                        if (CanMatchInRanked(tempList[i], tempList[j]))
                        {
                            player1 = tempList[i];
                            player2 = tempList[j];
                            found = true;
                        }
                    }
                }

                if (!found)
                    return null;

                if (player1 is null || player2 is null)
                    return null;

                // Rebuild queue without the two matched players
                var remaining = tempList.Where(p => p != player1 && p != player2).ToList();
                queue.Clear();
                foreach (var p in remaining)
                    queue.Enqueue(p);

                player1.LeaveQueue();
                player2.LeaveQueue();

                return new Match(player1, player2, mode);
            }
            else // QuickPlay
            {
                var tempList = queue.ToList();

                // Prefer closest skill match
                int bestI = -1, bestJ = -1;
                int bestDiff = int.MaxValue;
                for (int i = 0; i < tempList.Count; i++)
                {
                    for (int j = i + 1; j < tempList.Count; j++)
                    {
                        int diff = Math.Abs(tempList[i].SkillRating - tempList[j].SkillRating);
                        if (diff < bestDiff)
                        {
                            bestDiff = diff;
                            bestI = i;
                            bestJ = j;
                        }
                    }
                }

                if (bestI == -1)
                    return null;

                player1 = tempList[bestI];
                player2 = tempList[bestJ];

                if (player1 is null || player2 is null)
                    return null;

                // If queue is small, only allow reasonably close matches; otherwise allow any
                if (queue.Count <= 4 && bestDiff > 3)
                    return null;

                var remainingQuick = tempList.Where(p => p != player1 && p != player2).ToList();
                queue.Clear();
                foreach (var p in remainingQuick)
                    queue.Enqueue(p);

                player1.LeaveQueue();
                player2.LeaveQueue();

                return new Match(player1, player2, mode);
            }
        }

        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            // TODO: Implement this method
            // Hint: Very straightforward - simulate, record, display
            match.SimulateOutcome();
            matchHistory.Add(match);
            totalMatches++;
            Console.WriteLine(match.ToDetailedString());
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            // TODO: Implement this method
            // Hint: Loop through each queue and display formatted information
            Console.WriteLine("Current Queue Status 🎮");
            var queues = new Dictionary<string, Queue<Player>>{
                { "Casual", casualQueue },
                { "Ranked", rankedQueue },
                { "QuickPlay", quickPlayQueue }
            };
            foreach (var (name, queue) in queues){
                Console.WriteLine($"\n{name} Queue - {queue.Count} player(s)");
                if (queue.Count == 0){
                    Console.WriteLine(" (Empty)");
                    continue;
                }
                int position = 1;
                foreach (var player in queue)
                {
                    // Use Player.GetQueueTime() which returns a formatted string
                    var queueTimeDisplay = player.GetQueueTime();
                    Console.WriteLine($" {position}. {player.Username} - Waiting: {queueTimeDisplay}");
                    position++;
                }
            }
        }

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            // TODO: Implement this method
            // Hint: Combine player info with match history filtering
            Console.WriteLine(player.ToDetailedString());
            var inQueue = casualQueue.Contains(player) || rankedQueue.Contains(player) || quickPlayQueue.Contains(player);
            Console.WriteLine($"Queue Staus: {(inQueue ? "In Queue" : "Not in Queue")}");
            if (inQueue){
                var mode = casualQueue.Contains(player) ? GameMode.Casual :
                rankedQueue.Contains(player) ? GameMode.Ranked :
                GameMode.QuickPlay;
                var estimate = GetQueueEstimate(mode);
                Console.WriteLine($"Estimated Wait Time: {estimate}");
            }
            var recentMatches = matchHistory
                .Where(m => m.Player1 == player || m.Player2 == player)
                .OrderByDescending(m => m.MatchTime)
                .Take(3)
                .ToList();
                if (recentMatches.Count == 0){
                    Console.WriteLine("No matches have been played yet.");
                }

        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            // TODO: Implement this method
            // Hint: Check queue counts and apply mode-specific logic
            var queue = GetQueueByMode(mode);
            if (queue.Count >= 2)
            {
                return "There is no wait";
            }
            else if (queue.Count == 1)
            {
                return "The wait is short";
            }
            else{
                return "The wait is long";
            }
            
        }
        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}

}
