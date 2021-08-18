# Tetris Unity Tutorial - Extension

The intent of this Unity project is to extend the work of [Zigurous](https://www.youtube.com/user/Zigurous) in his [Unity Tetris Tutorial](https://www.youtube.com/watch?v=ODLzYI4d-J8). My work will start right at the same point where the tutorial ends.

All the credit goes to Zigurous [(Github profile)](https://github.com/zigurous), all I'm doing is extending their work for a learning purpose. Sprites are also their work as well.

<br />
<br />


# Roadmap

This is a small table that details my plans to make this fantastic project even better.

 - Idea: The core system/mechanic to implement/improve
 - Plan of action: How to tackle the idea, which resources I plan to use...
 - Why: Reasoning behind the idea
 - Priority: How high is the need for this idea in the project, where 🔴 is high, 🟡 is medium, and 🟢 is low.
 - Implemented: Offers a quick glance whether this feature is already implemented in the project or not, where ❌ is Not Yet Implemented, 🔧 is On the Works, and ✅ is Already Implemented

 I'll remove stuff as they are completed (will leave them there for a whole version though), and add some as it is needed.

| Idea | Plan of action | Why | Priority | Implemented |
| ---- | -------------- | --- |   :-:    |     :-:     |
| Better controls | Change the method that's used to get the user's input. Maybe instead of GetKeyDown, try GetKey | Without this, when we implement the difficulty changes, the game will be pretty much unplayable because of how slow the pieces will move. | 🔴 | ✅ |
| Show the next piece | Either make another tilemap and display them there, or take pictures of each piece and show them using some UI element. | This is a standard thing to have in these kinds of games | 🔴 | 🔧 |
| Scoring system | Use the UI elements in Unity to display a scoring system, and generate a score metric during gameplay | Adds replayability | 🟡 | ❌ |
| Difficulty System | While the game continues, the difficulty should increase little by little. The piece drop speed should increment as well. | Adds challenge to the game. As it is, the difficulty never really increases. | 🟡 | ❌|
| Save a piece for later | Maybe save the current piece into a variable | Another standard thing | 🟢 | ❌ |
| Main menu | This will probably be using the Unity Scene Manager I'd guess | It gives the game the feeling of being a game | 🟢 | ❌ |
| Music/SFX | I think this one shouldn't be too hard, maybe make a global settings script that holds these values | It makes the game feel less empty; a game is half visuals, half sound | 🟢 | ❌ |

