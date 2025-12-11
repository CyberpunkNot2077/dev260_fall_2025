# Project Design & Rationale

**Instructions:** Replace prompts with your content. Be specific and concise. If something doesn't apply, write "N/A" and explain briefly.

---

## Data Model & Entities

**Core entities:**  
_List your main entities with key fields, identifiers, and relationships (1–2 lines each)._

**Your Answer:**

**Entity A:**

- Name: CardCatalogue
- Key fields: Dictionary and List
- Identifiers: Card
- Relationships: CardAction and CardGame

**Entity B (if applicable):**

- Name: CardAction
- Key fields: Queue, List
- Identifiers: StartGame, EndGame, Damage, RecoverHealth, and more
- Relationships: CardCatalogue, and Card Game

**Identifiers (keys) and why they're chosen:**  
_Explain your choice of keys (e.g., string Id, composite key, case-insensitive, etc.)._

**Your Answer:**
These keys are what make this game as fun as it is tricky.


---

## Data Structures — Choices & Justification

_List only the meaningful data structures you chose. For each, state the purpose, the role it plays in your app, why it fits, and alternatives considered._

### Structure #1

**Chosen Data Structure:**  
_Name the data structure (e.g., Dictionary<string, Customer>)._


**Your Answer:**
Dictionary<Card>

**Purpose / Role in App:**  
_What user action or feature does it power?_


**Your Answer:**
The role of this app is to play a card game, where two players attempt to reduce the other's health to 0.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
Because when the opponent's health is 0, you win. But if your health is zero, you lose.


**Alternatives considered:**  
_List alternatives (e.g., List<T>, SortedDictionary, custom tree) and why you didn't choose them._

**Your Answer:**
Tree because this would have been the great alternative for the summon card and weapon card lists

---

### Structure #2

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**
List<Card>

**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
This contains the name, offense, and defense of the card in the deck that either the player or the opponent can use in the game.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
The summon list, weapon list, and ability list, all congregated into one big list, are all randomized. When the game starts, both the player and the opponent start with randomized 5 cards in their hands, so they have no idea what to expect.

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**


---

### Structure #3

**Chosen Data Structure:**  
_Name the data structure._

**Your Answer:**
The data structure is dubbed ShuffleDeck()
**Purpose / Role in App:**  
_What user action or feature does it power?_

**Your Answer:**
The ShuffleDeck() method is used for randomizing the whole deck before the match begins in the game.

**Why it fits:**  
_Explain access patterns, typical size, performance/Big-O, memory, simplicity._

**Your Answer:**
First, when the game starts, the deck is shuffled, so neither the player nor their opponent know what to expect upon drawing their first 5 cards to their hands.

**Alternatives considered:**  
_List alternatives and why you didn't choose them._

**Your Answer:**
HashSet() The reason why I did not consider this is because hashset might have mixed the cards alphabetically.

---

### Additional Structures (if applicable)

_Add more sections if you used additional structures like Queue for workflows, Stack for undo, HashSet for uniqueness, Graph for relationships, BST/SortedDictionary for ordered views, etc._

**Your Answer:**

---

## Comparers & String Handling

**Comparer choices:**  
_Explain what comparers you used and why (e.g., StringComparer.OrdinalIgnoreCase for keys)._

**Your Answer:**

**For keys:** 

**For display sorting (if different):** 
.ToList()

**Normalization rules:**  
_Describe how you normalize strings (trim whitespace, collapse duplicates, canonicalize casing)._

**Your Answer:**
For starters, I would trim the whitespace.

**Bad key examples avoided:**  
_List examples of bad key choices and why you avoided them (e.g., non-unique names, culture-varying text, trailing spaces, substrings that can change)._

---

## Performance Considerations

**Expected data scale:**  
_Describe the expected size of your data (e.g., 100 items, 10,000 items)._

**Your Answer:**

**Performance bottlenecks identified:**  
_List any potential performance issues and how you addressed them._

**Your Answer:**

**Big-O analysis of core operations:**  
_Provide time complexity for your main operations (Add, Search, List, Update, Delete)._

**Your Answer:**

- Add:
- Search:
- List:
- Update:
- Delete:

---

## Design Tradeoffs & Decisions

**Key design decisions:**  
_Explain major design choices and why you made them._

**Your Answer:**

**Tradeoffs made:**  
_Describe any tradeoffs between simplicity vs performance, memory vs speed, etc._

**Your Answer:**

**What you would do differently with more time:**  
_Reflect on what you might change or improve._

**Your Answer:**
