using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FileSystemNavigator
{
    /// <summary>
    /// Binary Search Tree implementation for File System Navigation
    /// 
    /// STUDENT ASSIGNMENT: Implement the TODO methods in this class
    /// This class demonstrates BST concepts through a practical file system simulation
    /// 
    /// Learning Objectives:
    /// - Apply BST operations to hierarchical data
    /// - Implement complex search and filtering operations  
    /// - Practice file system concepts through tree structures
    /// - Build practical navigation and management tools
    /// </summary>
    public class FileSystemBST
    {
        private TreeNode? root;
        private int operationCount;
        private DateTime sessionStart;

        public FileSystemBST()
        {
            root = null;
            operationCount = 0;
            sessionStart = DateTime.Now;
            
            Console.WriteLine("🗂️  File System Navigator Initialized!");
            Console.WriteLine("📁 BST-based file system ready for operations.\n");
        }

        // ============================================
        // 🚀 STUDENT TODO METHODS - IMPLEMENT THESE
        // ============================================

        /// <summary>
        /// TODO #1: Create a new file in the file system
        /// 
        /// Requirements:
        /// - Insert file into BST maintaining proper ordering
        /// - Use file name for BST comparison (case-insensitive)
        /// - Handle duplicate file names (return false if exists)
        /// - Set appropriate file metadata (size, dates, extension)
        /// 
        /// BST Learning: Insertion with custom comparison logic
        /// Real-World: File creation in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to create (e.g., "readme.txt")</param>
        /// <param name="size">File size in bytes (default 1024)</param>
        /// <returns>True if file created successfully, false if already exists</returns>
        public bool CreateFile(string fileName, long size = 1024)
        {
            operationCount++;
            
            // TODO: Implement file creation logic
            // Hints:
            // 1. Create FileNode with FileType.File and provided size
            // 2. Insert into BST using InsertNode helper method
            // 3. Handle duplicate file names (return false if exists)
            // 4. Extension will be automatically extracted in FileNode constructor
            
            // Check if file already exists
            if (SearchNode(root, fileName) != null)
            {
                return false;
            }
            
            // Create new file node and insert into BST
            var newFile = new FileNode(fileName, FileType.File, size);
            root = InsertNode(root, newFile);
            return true;
        }    

        /// <summary>
        /// TODO #2: Create a new directory in the file system
        /// 
        /// Requirements:
        /// - Insert directory into BST with FileType.Directory
        /// - Directories should sort before files with same name
        /// - Set size to 0 for directories (automatic in FileNode constructor)
        /// - Handle duplicate directory names
        /// 
        /// BST Learning: Custom comparison for different node types
        /// Real-World: Directory creation and organization
        /// </summary>
        /// <param name="directoryName">Name of directory to create (e.g., "Documents")</param>
        /// <returns>True if directory created successfully, false if already exists</returns>
        public bool CreateDirectory(string directoryName)
        {
            operationCount++;
            
            // TODO: Implement directory creation logic
            // Hints:
            // 1. Create FileNode with FileType.Directory
            // 2. Use same insertion logic as CreateFile but with different type
            // 3. Directories automatically have size = 0 and no extension
            
            // Check if directory already exists
            if (SearchNode(root, directoryName) != null)
            {
                return false;
            }
            
            // Create new directory node and insert into BST
            var newDir = new FileNode(directoryName, FileType.Directory);
            root = InsertNode(root, newDir);
            return true;
        }

        /// <summary>
        /// TODO #3: Find a specific file by exact name
        /// 
        /// Requirements:
        /// - Search BST efficiently using file name as key
        /// - Case-insensitive comparison
        /// - Return FileNode if found, null if not found
        /// - Use binary search to achieve O(log n) average time
        /// 
        /// BST Learning: Efficient searching in BST
        /// Real-World: File lookup in operating systems
        /// </summary>
        /// <param name="fileName">Name of file to find</param>
        /// <returns>FileNode if found, null otherwise</returns>
        public FileNode? FindFile(string fileName)
        {
            operationCount++;
            return SearchNode(root, fileName);
        }

        /// <summary>
        /// TODO #4: Find all files with a specific extension
        /// 
        /// Requirements:
        /// - Traverse entire BST collecting files with matching extension
        /// - Case-insensitive extension comparison (.txt = .TXT)
        /// - Return List of FileNode objects
        /// - Use in-order traversal for consistent ordering
        /// 
        /// BST Learning: Tree traversal with filtering
        /// Real-World: File type searches (find all .cs files)
        /// </summary>
        /// <param name="extension">File extension to search for (.txt, .cs, etc.)</param>
        /// <returns>List of files with matching extension</returns>
        public List<FileNode> FindFilesByExtension(string extension)
        {
            operationCount++;
            
            // TODO: Implement extension-based file search
            // Hints:
            // 1. Use TraverseAndCollect helper method
            // 2. Filter by FileType.File AND matching extension
            // 3. Handle extension format (with or without leading dot)
            var matchedFiles = new List<FileNode>();
            string ext = extension.StartsWith(".") ? extension : "." + extension;
            
            TraverseAndCollect(root, matchedFiles, file => 
                file.Type == FileType.File && 
                string.Equals(file.Extension, ext, StringComparison.OrdinalIgnoreCase));
            
            return matchedFiles;
        }

        /// <summary>
        /// TODO #5: Find all files within a size range
        /// 
        /// Requirements:
        /// - Traverse BST finding files with size between minSize and maxSize (inclusive)
        /// - Only include files, not directories
        /// - Return sorted list in ascending order by size
        /// - Handle edge cases (minSize > maxSize, etc.)
        /// 
        /// BST Learning: Complex filtering during traversal
        /// Real-World: Storage management and quota enforcement
        /// </summary>
        /// <param name="minSize">Minimum file size in bytes</param>
        /// <param name="maxSize">Maximum file size in bytes</param>
        /// <returns>List of files within size range, sorted by size</returns>
        public List<FileNode> FindFilesBySize(long minSize, long maxSize)
        {
            operationCount++;
            
            // TODO: Implement size-based file search
            // Hints:
            // 1. Use TraverseAndCollect with appropriate filter
            // 2. Check both minSize and maxSize bounds
            // 3. Only include FileType.File entries
            // 4. Sort results by size ascending
            var sizedFiles = new List<FileNode>();
            
            TraverseAndCollect(root, sizedFiles, file => 
                file.Type == FileType.File && 
                file.Size >= minSize && 
                file.Size <= maxSize);
            
            return sizedFiles.OrderBy(f => f.Size).ToList();
        }

        /// <summary>
        /// TODO #6: Find the largest files in the system
        /// 
        /// Requirements:
        /// - Find the N largest files by size
        /// - Only include files, not directories
        /// - Return list sorted in descending order by size
        /// - If fewer than N files exist, return all available
        /// 
        /// BST Learning: Aggregation and sorting during traversal
        /// Real-World: Finding storage hogs, backup prioritization
        /// </summary>
        /// <param name="count">Number of largest files to return</param>
        /// <returns>List of largest files, sorted descending by size</returns>
        public List<FileNode> FindLargestFiles(int count)
        {
            operationCount++;
            
            // TODO: Implement largest files search
            // Hints:
            // 1. Use TraverseAndCollect to get all files
            // 2. Filter for FileType.File only
            // 3. Sort descending by size
            // 4. Take top N items
            var allFiles = new List<FileNode>();
            
            TraverseAndCollect(root, allFiles, file => file.Type == FileType.File);
            
            return allFiles.OrderByDescending(f => f.Size).Take(count).ToList();
        }

        /// <summary>
        /// TODO #7: Calculate total size of all files in system
        /// 
        /// Requirements:
        /// - Sum total size of all files (directories don't count)
        /// - Use recursive approach matching BST structure
        /// - Return size in bytes
        /// 
        /// BST Learning: Aggregation through recursion
        /// Real-World: Disk usage calculation, storage reporting
        /// </summary>
        /// <returns>Total size of all files in bytes</returns>
        public long CalculateTotalSize()
        {
            operationCount++;
            return CalculateTotalSize(root);
        }

        private long CalculateTotalSize(TreeNode? node)
        {
            if (node == null)
            {
                return 0;
            }
            
            long currentSize = node.FileData.Type == FileType.File ? node.FileData.Size : 0;
            return currentSize + CalculateTotalSize(node.Left) + CalculateTotalSize(node.Right);
        }

        /// <summary>
        /// TODO #8: Delete a file or directory from the system
        /// 
        /// Requirements:
        /// - Remove file/directory from BST
        /// - Handle all three BST deletion cases properly
        /// - Return true if deletion succeeded, false if item not found
        /// - For two-child case, use inorder successor approach
        /// 
        /// BST Learning: Complex deletion with tree restructuring
        /// Real-World: File deletion in operating systems
        /// </summary>
        /// <param name="fileName">Name of file/directory to delete</param>
        /// <returns>True if deleted successfully, false if not found</returns>
        public bool DeleteItem(string fileName)
        {
            operationCount++;
            
            // TODO: Implement file/directory deletion
            // Hints:
            // 1. Find the node to delete first
            // 2. Handle three cases: no children, one child, two children
            // 3. For two children case, find inorder successor
            // 4. Update tree structure properly
            bool deleted = false;
            
            TreeNode? DeleteNode(TreeNode? node, string name)
            {
                if (node == null)
                {
                    return null;
                }
                
                int cmp = string.Compare(name, node.FileData.Name, StringComparison.OrdinalIgnoreCase);
                if (cmp < 0)
                {
                    node.Left = DeleteNode(node.Left, name);
                }
                else if (cmp > 0)
                {
                    node.Right = DeleteNode(node.Right, name);
                }
                else
                {
                    deleted = true;
                    // Node with only one child or no child
                    if (node.Left == null)
                    {
                        return node.Right;
                    }
                    else if (node.Right == null)
                    {
                        return node.Left;
                    }
                    else
                    {
                        // Node with two children: get the inorder successor
                        TreeNode successor = node.Right;
                        while (successor.Left != null)
                        {
                            successor = successor.Left;
                        }
                        // Copy successor's data to this node
                        node.FileData = successor.FileData;
                        // Delete the successor
                        node.Right = DeleteNode(node.Right, successor.FileData.Name);
                    }
                }
                return node;
            }
            
            root = DeleteNode(root, fileName);
            return deleted;
        }

        // ============================================
        // 🔧 HELPER METHODS FOR TODO IMPLEMENTATION
        // ============================================

        /// <summary>
        /// Helper method for BST insertion
        /// Students should use this in CreateFile and CreateDirectory
        /// </summary>
        private TreeNode? InsertNode(TreeNode? node, FileNode fileData)
        {
            // TODO: Implement recursive BST insertion
            // Base case: if node is null, create new TreeNode
            // Recursive case: compare names and go left or right
            // Use CompareFileNodes for proper ordering
            if (node == null)
            {
                return new TreeNode(fileData);
            }
            else if (CompareFileNodes(fileData, node.FileData) < 0)
            {
                node.Left = InsertNode(node.Left, fileData);
            }
            else
            {
                node.Right = InsertNode(node.Right, fileData);
            }
            return node;
        }
        /// <summary>
        /// Helper method for BST searching
        /// Students should use this in FindFile
        /// </summary>
        private FileNode? SearchNode(TreeNode? node, string fileName)
        {
            // TODO: Implement recursive BST search
            // Base case: if node is null, return null
            // Base case: if names match, return node.FileData
            // Recursive case: compare names and go left or right
            if (node == null)
            {
                return null;
            }
            else if (fileName == node.FileData.Name)
            {
                return node.FileData;
            }
            else if (string.Compare(fileName, node.FileData.Name, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return SearchNode(node.Left, fileName);
            }
            else
            {
                return SearchNode(node.Right, fileName);
            }
        }


        /// <summary>
        /// Helper method for collecting nodes during traversal
        /// Students should use this for FindFilesByExtension, FindFilesBySize, etc.
        /// </summary>
        private void TraverseAndCollect(TreeNode? node, List<FileNode> collection, Func<FileNode, bool> filter)
        {
            // TODO: Implement in-order traversal with filtering
            // Base case: if node is null, return
            // Recursive case: traverse left, process current, traverse right
            // Add to collection only if filter returns true
            if (node == null)
            {
                return;
            }
            TraverseAndCollect(node.Left, collection, filter);
            if (filter(node.FileData))
            {
                collection.Add(node.FileData);
            }
            TraverseAndCollect(node.Right, collection, filter);
        }

        private int CompareFileNodes(FileNode a, FileNode b)
        {
            // Directories sort before files
            if (a.Type != b.Type)
                return a.Type == FileType.Directory ? -1 : 1;
            
            // Then alphabetical by name (case-insensitive)
            return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        }

        // ============================================
        // 🎯 PROVIDED UTILITY METHODS
        // ============================================

        /// <summary>
        /// Display the file system tree structure visually
        /// Helps students visualize their BST structure
        /// </summary>
        public void DisplayTree()
        {
            Console.WriteLine("🌳 File System Tree Structure:");
            Console.WriteLine("================================");
            
            if (root == null)
            {
                Console.WriteLine("   (Empty file system)");
                return;
            }
            DisplayTreeEnhanced(root, "", true, true);
            Console.WriteLine("================================\n");
            Console.WriteLine("🌲 Horizontal Level-by-Level View:");
            DisplayTreeByLevels();
        }

        /// <summary>
        /// Enhanced tree display with better visual formatting and clear parent-child relationships
        /// </summary>
        private void DisplayTreeEnhanced(TreeNode? node, string prefix, bool isLast, bool isRoot)
        {
            if (node == null) return;

            // Display current node with enhanced formatting
            string connector = isRoot ? "🌟 " : (isLast ? "└── " : "├── ");
            string nodeInfo = $"{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : $" ({FormatSize(node.FileData.Size)})")}";
            
            Console.WriteLine(prefix + connector + nodeInfo);

            // Update prefix for children
            string childPrefix = prefix + (isRoot ? "" : (isLast ? "    " : "│   "));

            // Display children with clear Left/Right indicators
            bool hasLeft = node.Left != null;
            bool hasRight = node.Right != null;

            if (hasRight)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "├─(R)─┐");
                DisplayTreeEnhanced(node.Right, childPrefix + "│     ", !hasLeft, false);
            }

            if (hasLeft)
            {
                Console.WriteLine(childPrefix + "│");
                Console.WriteLine(childPrefix + "└─(L)─┐");
                DisplayTreeEnhanced(node.Left, childPrefix + "      ", true, false);
            }
        }

        /// <summary>
        /// Display tree in a horizontal level-by-level format
        /// </summary>
        private void DisplayTreeByLevels()
        {
            if (root == null) return;

            var queue = new Queue<(TreeNode?, int)>();
            queue.Enqueue((root, 0));
            int currentLevel = -1;

            while (queue.Count > 0)
            {
                var (node, level) = queue.Dequeue();
                
                if (level > currentLevel)
                {
                    if (currentLevel >= 0) Console.WriteLine();
                    Console.Write($"Level {level}: ");
                    currentLevel = level;
                }

                if (node != null)
                {
                    Console.Write($"[{node.FileData.Name}{(node.FileData.Type == FileType.Directory ? "/" : "")}] ");
                    queue.Enqueue((node.Left, level + 1));
                    queue.Enqueue((node.Right, level + 1));
                }
                else
                {
                    Console.Write("[null] ");
                }
            }
            Console.WriteLine();
        }


        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes}B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024}KB";
            return $"{bytes / (1024 * 1024)}MB";
        }

        /// <summary>
        /// Get comprehensive statistics about the file system
        /// </summary>
        public FileSystemStats GetStatistics()
        {
            var stats = new FileSystemStats
            {
                TotalOperations = operationCount,
                SessionDuration = DateTime.Now - sessionStart
            };

            if (root != null)
            {
                CalculateStats(root, stats);
            }

            return stats;
        }

        private void CalculateStats(TreeNode? node, FileSystemStats stats)
        {
            if (node == null) return;

            var file = node.FileData;
            if (file.Type == FileType.File)
            {
                stats.TotalFiles++;
                stats.TotalSize += file.Size;
                
                if (file.Size > stats.LargestFileSize)
                {
                    stats.LargestFileSize = file.Size;
                    stats.LargestFile = file.Name;
                }
            }
            else
            {
                stats.TotalDirectories++;
            }

            CalculateStats(node.Left, stats);
            CalculateStats(node.Right, stats);
        }

        /// <summary>
        /// Check if the file system is empty
        /// </summary>
        public bool IsEmpty() => root == null;

        /// <summary>
        /// Load sample data for testing and demonstration
        /// </summary>
        public void LoadSampleData()
        {
            Console.WriteLine("📁 Loading sample file system data...");
            
            // Sample directories
            var sampleDirs = new[]
            {
                "Documents", "Pictures", "Videos", "Music", "Downloads",
                "Projects", "Code", "Images", "Archive"
            };

            // Sample files with extensions and sizes
            var sampleFiles = new[]
            {
                ("readme.txt", 2048L), ("config.json", 1024L), ("app.cs", 5120L),
                ("photo.jpg", 2048000L), ("song.mp3", 4096000L), ("video.mp4", 52428800L),
                ("document.pdf", 1048576L), ("presentation.pptx", 3145728L),
                ("spreadsheet.xlsx", 512000L), ("archive.zip", 10485760L)
            };

            try
            {
                // Create directories
                foreach (var dir in sampleDirs.Take(6))
                {
                    CreateDirectory(dir);
                }

                // Create files
                foreach (var (fileName, size) in sampleFiles.Take(8))
                {
                    CreateFile(fileName, size);
                }

                Console.WriteLine("✅ Sample data loaded successfully!");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  Cannot load sample data - TODO methods not implemented yet");
            }
        }
    }
}
