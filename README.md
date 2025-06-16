                                                                 ** # Chest-System**   

The ultimate loot-based adventure where every chest holds a surprise! Earn, collect, and unlock a wide variety of chests filled with coins, gems, power-ups, rare items, and more.   

what I did: Designed and implemented a dynamic chest reward system using Unity and C#. Utilized the MVC architecture and State Pattern to manage chest behavior (Locked, Unlocking, Opened). Employed ScriptableObjects for scalable and data-driven configuration of chest types and rewards. Integrated Singleton and Service patterns for game-wide resource and currency handling. Enhanced performance using object pooling for chest GameObjects. This modular and reusable system allows easy expansion and fine-tuning, making it a robust part of the game's progression mechanic.   

**GENRE** : Puzzle, Adventure, Learning    
    
**Technologies** :        
•	Unity Engine for development   
•	C# for scripting   
•	TextMesh Pro for advanced UI rendering   
•	Unity’s ScriptableObject System for chest data management   
•	Unity Prefabs for reusable chest visuals   
•	Unity Scenes for gameplay environment   

**Design Patterns**   
1.	MVC Architecture (Model-View-Controller):   
    •	Model: ChestModel.cs handles chest data like type and timer.  
    •	View: ChestView.cs manages the UI and visual representation.   
    •	Controller: ChestController.cs links the view with the model and manages user interactions.    
2.	State Pattern:   
    •	Encapsulated chest behavior in different states like Locked, Unlocking, and Opened.   
    •	Classes: ChestLockedState.cs, ChestUnLockingState.cs, ChestOpenedState.cs, and base ChestState.cs.    
3.	ScriptableObjects:    
    •	Used for chest configuration (ChestScriptableObject.cs) and storing all chest types (ChestScriptableObjectList.cs).     
    •	Promotes data-driven design and easier balancing.    
4.	Singleton Pattern:    
    •	GenericMonoSingleton.cs provides global access to services like CurrencyService and ChestService.   
5.	Service Layer:    
    •	ChestService.cs and CurrencyService.cs abstract the business logic, promoting clean separation of concerns.   
6.	Object Pooling:    
    •	ChestObjectPool.cs helps in efficient memory management by reusing chest GameObjects.

**Images**
 ![](https://github.com/Sega-13/Chest-System/blob/main/Images/Screenshot%202024-10-09%20101631.png) ![](https://github.com/Sega-13/Chest-System/blob/main/Images/Screenshot%202024-10-09%20101717.png)
                                                                 
