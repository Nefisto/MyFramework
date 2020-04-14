# My framework



| Quick reference                                              |
| :----------------------------------------------------------- |
| [Variables architecture](#variables-architecture:-(ryan-hipple---unite-2017)) |
| [Game event architecture](#Game Event architecture: (Ryan Hipple - Unite 2017)) |
| [Runtime sets approach](#Runtime sets: (Ryan Hipple - Unite 2017)) |


### Notes:

* Credits are given from the first time that I heard about this practices, examples are not the same from conferences, I've made my own changes, so... 

### Credits:

* [Unite Austin 2017 - Game Architecture with Scriptable Objects (Ryan Hipple talk)](https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=2713s) 

## [Variables architecture: (Ryan Hipple - Unite 2017)](#My framework)

#### Why use it

​	Variables **(int, vector, boolean ...)** used to create an abstraction layer between objects that share states or info

#### When use it

​	If u have some state that other object need to know about, use it to make this communication layer

#### How its work

​	This architecture are divided in 3 parts:

1. A base class that define description n values, also an getter that will chose between an original value or an runtime value, it's necessary because *Scriptable Object* don't reset values, so, any change is permanent

   > OBS: if *haveDefaultValue* is true, value will represent the default value.

   ```c#
   public abstract class BaseVariable<T> : ScriptableObject
   {
       private string DeveloperDescription = "";
   
       [Header("Will have an default value?")]
       public bool haveDefaultValue = false;
       
       [SerializeField]
       private T value;
       private T runTimeValue;
       
       public T Value
       {
           get => haveDefaultValue ? runTimeValue : value;
           set
           {
               if (haveDefaultValue)
                   runTimeValue = value;
               else
                   this.value = value;
           }
       }
   
       private void OnEnable()
       {
           if (haveDefaultValue)
               runTimeValue = value;
       }
   }
   ```

2. An *reference monobehaviour* that will be attached to objects to create communication between shared variables(states) without require any kind of rigid references. Also, this is what will make the system *designer-friendly*.

   > Vector2Int example

   ```c#
   using System;
   using UnityEngine;
   
   [Serializable]
   public class Vector2IntReference
   {
       public bool UseConstant = true;
       public Vector2Int ConstantValue;
       public Vector2IntVariable Variable;
   
       #region Constructors
   
       public Vector2IntReference()
       { }
   
       public Vector2IntReference(Vector2Int value)
       {
           UseConstant = true;
           ConstantValue = value;
       }
   
       #endregion
   
       #region Properties
   
       public Vector2Int Value
       {
           get => UseConstant ? ConstantValue : Variable.Value;
       }
   
       public int x
       {
           get => Value.x;
           set
           {
               if (UseConstant)
                   ConstantValue.x = value;
               else
                   Variable.x = value;
           }
       }
   
       public int y
       {
           get => Value.y;
           set
           {
               if (UseConstant)
                   ConstantValue.y = value;
               else
                   Variable.y = value;
           }
       }
       
       #endregion
   
       public static implicit operator Vector2Int(Vector2IntReference reference)
           => reference.Value;
   }
   ```

3. To finish, an property drawer to turn reference in editor more friendly

   ```c#
   using UnityEngine;
   using UnityEditor;
   
   public abstract class BaseReferenceDrawer : PropertyDrawer
   {
       /// <summary>
       /// Options to display in the popup to select constant or variable.
       /// </summary>
       private readonly string[] popupOptions =
           { "Use Constant", "Use Variable" };
   
       /// <summary> Cached style to use to draw the popup button. </summary>
       private GUIStyle popupStyle;
   
       public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
       {
           if (popupStyle == null)
           {
               popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
               popupStyle.imagePosition = ImagePosition.ImageOnly;
           }
   
           label = EditorGUI.BeginProperty(position, label, property);
           position = EditorGUI.PrefixLabel(position, label);
   
           EditorGUI.BeginChangeCheck();
   
           // Get properties
           SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
           SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
           SerializedProperty variable = property.FindPropertyRelative("Variable");
   
           // Calculate rect for configuration button
           Rect buttonRect = new Rect(position);
           buttonRect.yMin += popupStyle.margin.top;
           buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
           position.xMin = buttonRect.xMax;
   
           // Store old indent level and set it to 0, the PrefixLabel takes care of it
           int indent = EditorGUI.indentLevel;
           EditorGUI.indentLevel = 0;
   
           int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);
   
           useConstant.boolValue = result == 0;
   
           EditorGUI.PropertyField(position,
               useConstant.boolValue ? constantValue : variable,
               GUIContent.none);
   
           if (EditorGUI.EndChangeCheck())
               property.serializedObject.ApplyModifiedProperties();
   
           EditorGUI.indentLevel = indent;
           EditorGUI.EndProperty();
       }
   }
   ```

## [Game Event architecture: (Ryan Hipple - Unite 2017)](#My framework)

#### What

​	An triggers that centralize behaviors between objects in a same place.

#### How

​	Any object that has an *GameEventListener* can *listen* to an call made by an *GameEvent*, so add GameEventListener to your object, set the GameEvent and add functions to say *what it will do when this game event happen*.

#### When

​	If u can isolate an event, than u can use it. Maybe an *OnInit* that setup all things before game start or an *OnPause* that show some UI and inventory info or maybe *OnDie* that triggers things that happen when player die

## [Runtime sets: (Ryan Hipple - Unite 2017)](#My framework)

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
