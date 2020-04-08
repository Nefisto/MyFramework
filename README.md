# My framework



## Variables architecture: (Ryan Hipple - Unite 2017) 

​	Used to create an abstraction layer between objects that shar states

## Game Event architecture: (Ryan Hipple - Unite 2017)

#### What

​	An triggers that centralize behaviors between objects in a same place.

#### How

​	Any object that has an *GameEventListener* can *listen* to an call made by an *GameEvent*, so add GameEventListener to your object, set the GameEvent and add functions to say *what it will do when this game event happen*.

#### When

​	If u can isolate an event, than u can use it. Maybe an *OnInit* that setup all things before game start or an *OnPause* that show some UI and inventory info or maybe *OnDie* that triggers things that happen when player die

## Runtime sets: (Ryan Hipple - Unite 2017)

#### What

​	Instead of use singletons or other object that is dependent of scene or third other objects, use this scriptable object to control your runtime group of *things*

#### How

​	It's an abstract class:

```c#
public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> Items = new List<T>();

    public void Add(T thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
    }

    public void Remove(T thing)
        => Items.Remove(thing);
}
```

You need to create an class of *things*, it will be your *ScriptableObject* (asset) part, lets create bullets:

```c#
[CreateAssetMenu]
public class BulletsRuntimeSet : RuntimeSet<TrackableBullet> { }
```

And this is the *Monobehaviour* (component) part that will be added to anything that you want to track as a bullet, a *Bullet* need to *register* and *unregister* when *enabled* n *disabled*, respectively, so.

```c#
public class TrackableBullet : MonoBehaviour
{
    public BulletsRuntimeSet Set;

    private void OnEnable()
        => runtimeSet.Add(this);

    private void OnDisable()
        => runtimeSet.Remove(this);
}
```

Now, any object that have *TrackableBullet* component will be tracked in runtime, so if u need any info from it container,  only iterate.

#### When

​	When u need to control things in real time, lets say, amount of candies or quantity of flying enemies... then u use it. Remember that an object can be trackable by many sets without worry, if u disable it in an place, u remove from all sets. 