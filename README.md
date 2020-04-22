# Framework

> Framework para otimizar o fluxo de desenvolvimento quando se usa unity



| Sumario                                               |
| :---------------------------------------------------- |
| [Arquitetura de variaveis](#arquitetura-de-variáveis) |
| [Arquitetura de eventos](#arquitetura-de-eventos)     |
| [Runtime sets](#runtime-sets)                         |
| [Reload-proof singletons](#reload-proof-singleton)    |

### Notes:

* O código se difere da sua versão original a qual o credito é dado, o mesmo representa apenas o local onde eu ouvi falar sobre o conceito pela primeira vez.
* A parte de **como funciona** expressa apenas a ideia central de como o código esta estruturado, para mais informações leia o código.

### Créditos:

* [Ryan Hipple - Unite 2017](https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=2713s)

  > Variáveis, runtime sets, game events

* [Richard Fine - Unite 2016](https://www.youtube.com/watch?v=6vmRwLYWNRo) 

  > Reload-proof singletons, audio events

---

## Arquitetura de variáveis

> Ryan Hipple - Unite 2017

#### Conceito

​	Variáveis como *scriptable objects* para criar uma camada de comunicação entre os objetos que dependem desse estado compartilhado

#### Quando usar

​	Caso uma variável local qualquer seja necessária para o comportamento de algum outro objeto, ao invés de criar uma referencia entre os objetos em questão, torne a variável um SO e os objetos se referenciam a ele pelos assets

#### Como usar

1. Crie um novo SO do tipo de variável desejada
2. Dentro do script que ira utilizar da variável compartilhada: `public IntReference myRef`
3. No *inspector* selecione "*use variable*" (veja imagem mais abaixo)
4. Insira a variável criada no passo 1

#### Como funciona

​	A arquitetura é dividida em 3 partes

1. Uma classe base genérica

```c#
public abstract class BaseVariable<T> : ScriptableObject
{
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
    
    private void OnDisable()
    {
        if (haveDefaultValue)
            runTimeValue = value;
    }
}
```

​	E um SO para cada tipo de variável que for criada

```c#
[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/Int")]
public class IntVariable : BaseVariable<int>
{ }
```

2. Uma referencie que será criada nos monobehaviours que forem utilizar da variável em questão, sem criar ligações rígidas entre os interessados. Essa classe também tem como objetivo criar um sistema mais *designer-friendly* tornando possível que os estados compartilhados sejam alterados sem a necessidade de alteração no codigo, o inspetor do unity funciona como um injetor

```c#
using System;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public IntReference()
    { }

    public IntReference(int value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public int Value
    {
        get => UseConstant ? ConstantValue : Variable.Value; 
        set
        {
            if (UseConstant)
                ConstantValue = value;
            else
                Variable.Value = value;
        }
    }

    public static implicit operator int(IntReference reference)
        => reference.Value;
}
```

E para melhorar a experiência, é criado um PropertyDrawer

![Imgur](https://i.imgur.com/bBUb3VH.png)

[to up](#framework)

---

## Arquitetura de eventos

> Ryan Hipple - Unite 2017

#### Conceito

​	Destacar eventos que acontecem durante a execução, como por exemplo um pause, e criar uma camada que separa os objetos que são responsáveis por disparar o evento (triggers) e aqueles que respondem a esse evento (listeners)

#### Quando usar

​	Sempre que possível

#### Como usar

1. Crie um novo SO do tipo de evento desejado 

2. Dentro do objeto que ira atuar como *trigger*, crie a variável e faça seu *invoke* no lugar desejado

   ```c#
   ...
   public GameEvent onDeath;
   ...
   public void Death()
       => onDeath.Raise()
   ```

3. Nos objetos que atuarão como *listeners*, adicione o componente *GameEventListener*
   ![Imgur](https://i.imgur.com/YSVD9sk.png)

4. Arraste o evento ao qual o objeto ira responder e insira os comportamentos a serem executados na lista de *response*

> OBS: Não há garantia de execução para a resposta (até onde eu sei)

#### Como funciona

1. Uma classe que representa o evento e expõe maneiras de se registrar e se remover como listener do evento.

   > OBS: Essa classe é recriada para cada evento tipado distinto

```c#
[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent(void)")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = new List<GameEventListenerInt>();

    public void Raise()
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
```

​	Para facilitar o trabalho de *debugar*, é escrito um editor que permite que o evento seja invocado do *inspector*

![Imgur](https://i.imgur.com/KgJvQRY.png)

> OBS: por razões obvias o evento só pode ser chamado em *runtime*

2. Para o lado do *runtime*, há um componente de *monobehaviour* que atua como listener para um evento qualquer, do mesmo tipo, e executa os eventos quando o *trigger* é ativado

   > OBS: Para eventos tipados é necessário criar uma classes serializavel que herde de *UnityEvent* 
   >
   > ex:  Para um evento de *int*
   >
   > ```c#
   > [Serializable]
   > public class MyIntEvent : UnityEvent<int> {}
   > ```

```c#
public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
```



[to up](#framework)

---

## Runtime sets

> Ryan Hipple - Unite 2017

#### Conceito

​	As vezes é necessário manter um registro de quais objetos específicos existem em cena em um momento qualquer, ou talvez criar categorias para esses objetos e ter acesso a diferentes grupos em tempo de execução. Para solucionar esse problema sem usar variáveis globais de controle (AKA: Singletons, que causam dependências na comunicação)

#### Quando usar

​	Sempre que for necessário categorizar e/ou acompanhar objetos em tempo de execução

#### Como usar

1. Crie o SO de *RuntimeSet*

2. Aos objetos que farão parte desse grupo, insira o componente *Runtime Item* e adicione o grupo a qual o item pertence

3. No script em que for necessário fazer referencia ao grupo, adicione ele como uma variável publica a ser injetada pelo *inspector* (O uso com LINQ é apenas um exemplo)

   ```c#
   ...
   public RuntimeSet trackableGroup;
   ...
   trackableGroup.Items.Find(...);
   ```

#### Como funciona

1. Uma classe abstrata que representa um grupo de "coisas"

```c#
[CreateAssetMenu(fileName= "RuntimeSet", menuName= "RuntimeSet")]
public class RuntimeSet : ScriptableObject
{
    public List<RuntimeItem> Items = new List<RuntimeItem>();

    public void Add(RuntimeItem thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
    }

    public void Remove(RuntimeItem thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);
    }
}
```

2. Um componente que marca um objeto como "rastreável"

```c#
public class RuntimeItem : MonoBehaviour
{
    public RuntimeSet runtimeSet;

    private void OnEnable()
        => runtimeSet.Add(this);

    private void OnDisable()
        => runtimeSet.Remove(this);
}
```

> OBS: Quem acessar o componente é responsável por coletar as informações necessárias

[to up](#framework)

---

## Reload-proof Singleton

> Richard Fine - Unite 2016
>

#### Conceito

​	Em algumas situações precisamos de objetos que existam independentemente de qual cena está em uso atualmente, para esses casos é proposto o uso de padrões singletons que existem apenas nos assets por meio de SO. Essa abordagem evita o uso de *preload-scenes*

#### Como usar

1. Crie um script que herde de *ScriptableSingleton<T>*

```c#
[CreateAssetMenu(fileName = "SceneController", menuName = "Managers/Scene")]
public class SceneController : ScriptableSingleton<SceneController>
{   
    public void ChangeScene(int sceneIndex)
       => SceneManager.LoadScene(sceneIndex);
}
```

2. **Crie uma pasta chamada "Resources" em qualquer parte do seu diretório e deixa o singleton ali dentro**, caso seja necessário testar diferentes "controladores", troque o controlador que está dentro da pasta
3. A partir de qualquer ponto do código de qualquer *script*, chame o *singleton*: `SceneController.Instance.ChangeScene(0)` 

#### Como funciona

 1. Uma classe genérica que procura o *singleton* dentro da pasta *resource* 

    ```c#
    using System.Linq;
    using UnityEngine;
    
    public abstract class ScriptableSingleton<T> : ScriptableObject 
        where T : ScriptableObject
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var type = typeof(T);
                    var instances = Resources.LoadAll<T>(string.Empty);
                    _instance = instances.FirstOrDefault();
                    if (_instance == null)
                    {
                        Debug.LogErrorFormat("[ScriptableSingleton] No instance of {0} found!", type.ToString());
                    }
                    else if (instances.Count() > 1)
                    {
                        Debug.LogErrorFormat("[ScriptableSingleton] Multiple instances of {0} found!", type.ToString());
                    }
                }
                return _instance;
            }
        }
    }
    ```

[to up](#framework)