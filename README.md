**Assignment 1 for Introduction to programming for games**

---

## Title - The Rage of the Gods. 
A 3D First-Person-Shooter game about a wizard with magic spells trying to survive waves of enemies.

The game premise takes place in a world where wizards have enraged the gods and have declared war on them. It is about a young wizard called “Reynold”. In the demo, he is on his way to the magic temple of “Firestorm” to look for answers about his lost father and his relationship with magic.  The Rage of the Gods is a fast paced first person shooter game where the player fights against three types of enemies. The enemies spawn in rounds and the player is trying to survive for as long as possible. 
	The following text describes how the project works behind the scenes. How the systems are organised and communicate with each other. Then, it explains how feedback from playtesting shaped the development of the game and provides evidence for it. Finally, it analyses the technical difficulties presented during the development cycle and what approaches were taken to solve them. 

## Characters

**Player**
Reynold - a Hogwarts drop out - is a wizard who has travelled and seen a lot. He has mastered one spell, the fireball. He is keen on learning new spells however, including one that will enable him to protect himself instead of using his agility to dodge everything that comes towards him.



**Kamikaze** - Enemy
The Kamikaze are programmed to chase the player and get as close to him as possible. Then, they charge and explode dealing massive amounts of damage. On the flip side, they move fast but their health is very low so the player can easily exterminate him before they get too close.

**Wizard** - Enemy
Wizards were first created and then mobilized by the gods to defeat the wizards on earth. They shoot magic projectiles that do not deal too much damage. They also have a medium amount of health. If the player lets them to grow in numbers, they can quickly become overwhelming by pinning down the player. 



**Tank** - Enemy
The tank enemy has got the most amount of health and a decent amount of damage but is very slow. If it gets close enough to the player, it will jump up and stomp down. Depending on where the player was the moment it jumped up, it will spawn metal spikes that emerge from the ground when it stomps down.



## TECHNICAL OVERVIEW
In this section, important scripts are analysed. A short description and flow of processes is provided for the scripts. 

**ThrowSpell.cs**

The script is including the player shooting mechanic but also describes how a projectile should behave when shot. The way it is implemented is that it checks if Input.GetMouseButtonDown(0) is true in the Update method. It then calls the Shoot() method which casts a ray from the centre of the camera (see Figure 2) . If it hits anything, it stores the hit.point to a vector3 called m_Destination. Otherwise, if nothing was hit, the ray stores a point that is 1000 units along the ray. A projectile prefab is then instantiated on the spawning point. By then getting its Rigidbody component, we can change its velocity. Setting the direction by getting the normalised vector from the destination variable and the spawning point. We can then multiply by a certain speed to make it move towards that direction. Moreover, an animation for making the wand go down and up in quick succession is called. With it, a flame sound effect for the fireball.
 
 

**DealDamage.cs**

This script detects a collision between a projectile and a receiver and then calls another script. In this case, dealing damage to the receiver. If the projectile does not hit anything within a time limit, the projectile is destroyed. If, on the other hand, collides with anything, we check that it has not collided with itself or the shooter. It is worth noting that the script takes two strings. One for the shooter and one for the receiver. This is important because we need to know who is who so we can invoke the right TakeDamage() function. A switch statement is used for this. Depending on who the receiver is, either the player or the enemy, the right method is called (see Figure 3).  Subsequently, we instantiate a particle effect on the point of impact, and we let it play its animation before destroying both objects.
 


**SpawnEnemies.cs**

This script is responsible for spawning in waves different types of enemies from certain spawning points. A separate class is also defined in the script. The class is for adding weighted probability to each game object I add to the list of enemies I am going to spawn. [System.Serializable] is used for showing the properties of the class on the inspector.
 

A list of type ProbabilityEnemy holds all the potential enemy game objects. In the inspector, we can set the probability of each enemy game object in the list. Adding all the maxProbabilityRange values together should sum up to a certain threshold. In this case I used 100. For example, one enemy has range 0-40, second enemy has range 41-60 and third enemy is from 61-100. When it is time to grab a random enemy from the list, a random number ranging from 0 to 100 is generated and compared against the minimum and maximum probability range of each element in the list. If the random number fits within the enemy’s probability range, then that enemy is returned. The potential pitfall I saw with this approach is that all enemies in the list are checked against a number. This means that the longer the list, the bigger the overhead. Three types of enemies are used in this demo, so the overhead is close to none.
   







For the wave system, I used an Enum for managing all the spawning states. The state machine consisting of counting, spawning, and waiting states (see diagram 1). The counting state is for counting down before a wave commences. The spawning state is for picking a random enemy type and instantiating it at a random spawn point. Finally, the waiting state is for when all the enemies have spawned but the games wait the player to kill them or be killed.







The game begins on counting state and counts down before the first wave. It then switches to spawning state and starts spawning enemies. The maximum number of enemies spawned starts at two and increases by two with each wave. After all enemies have been spawned, the game switches to waiting state and checks in one second intervals if all enemies are dead so it can start counting down for the next wave. The first implementation of this check was taking place with Object.FindObjectOfType(). However, I considered this approach to be very expensive since it goes through every single object in the scene just to check that all enemies are alive so it can repeat the process again. I changed my solution to instead of checking for every object in the scene, I am using a list of alive enemies that is filled with each spawned enemy. When an enemy dies, it removes itself from the list. When the list is empty, all enemies for that wave are dead.  


---

