## Кратко о главном

1. **Общение с hr**. Проверка базовых софт-скиллов, проверка коммуникативности и вежливости.

2. Возможно **тестовое задание**. Зависит от специфики компании и задач, которые в ней решают. В любом случае, по backend'у может быть задание на ASP, работу с БД или решение алгоритмических задач. Для последнего важно набить руку в решении задачек с LeetCode или CodeWars, а также почитать книги, по типу "Грокаем алгоритмы", также неплохо было бы узнать про оценку скорости алгоритма (О большое) и почитать больше статей на различных сайтах.

3. **Техническое собеседование**. На джуна задают вопросы на базовое знание .NET, CLR, IL, C# и тд. Все это я собрал и разобрал ниже. Опять таки, здесь главное погуглить "Собес C# <paste_your_level>" и повторить все, что изучал за годы обучения.

4. **Дополнительно**. Данный этап может быть озаглавлен по разному, если нижеописанные вопросы встретятся перед техническим заданием/интервью. Типичный рассказ о себе, man из компании также расскажет о ней, чем она занимается и тд. Следует заранее узать больше подробностей о компании, внимательно выслушать собеседника и задать несколько вопросов по поводу работы, огранизации рабочего процесса, коммуникации в команде и тп. Например, 

   * спросить соблюдается ли work-life balance, 
   * как обстоят дела с переработками и как к этому относятся. 
   * Также узнать, над какими проектами мне придется работать, 
   * нужно ли их документировать или нужно ли поддерживать существующую документацию. 
   * Используются ли в компании при разработке собственные библиотеки, например, при работе с БД или предпочтения отдаются готовым сторонним решениям. 
   * Как устроен рабочий процесс, как выдются задачи. 
   * В вакансии указано, что вы нанимаете людей с высшим образованием, однако я обладаю средним профессиональным
   * Что можете сказать по поводу моего GitHub

   При рассказе о себе не стоит цитировать и пересказывать собственное резюме. Важно рассказать суть - как я начал и на каком этапе сейчас нахожусь, где работал, какие задачи решал. К концу интервью, будем надеяться, придется рассказать о коммерческом опыте разработки. Для этого стоит приберечь пару тру стори с работы, возможно, нестандартное решение проблемы или как я справлялся с непонятными задачами, какие инструменты применял.



## Разбор части 3 - база языка, общие концепции

## Junior / Junior+

**CLR, IL, CLS**

**CLR** ("Common Language Runtime", "общеязыковая исполняющая среда") - это компонент .NET Framework, основной задачей которого является управление интерпретацией и исполнением кода IL. CLR отвечает за изоляцию памяти приложений, проверку типов, безопасность кода, преобразование IL в машинный код.

**IL** (Intermediate Language) - код, содержащий набор инструкций, не зависящих от платформы. Иными словами, после компиляции исходного кода он преобразуется не в код для какой-то определенной платформы, а в промежуточный код на языке IL.

**CLS** ("Common Language Specification", общеязыковая спецификация) - это набор правил, следуя которым разработчики достигают бесконфликтной работы во всех языках .NET.

#### В чем различие между Value Type и Reference Type?

`Value Type` находятся в стеке, а `Reference Type` в куче. Также бывают очень большие объекты и если размер одно из таких превышает 85000 байт, то он помещается в `Large Object Heap`

#### Что такое using, где используется и во что превращается после компиляции?

Конструкция `using` - синтаксический сахар.

Применятся при 1) подключении пространств имен, 2) для обеспечения безопасной обработки объектов IDisposable - контроль ресурсов. 3) (C# 10) Добавление модификатора `global` к директиве `using` означает, что директива using должна применяться ко всем файлам в компиляции. Пример: `global using` 4) (C# 6) Директива `using static` указывает тип, доступ к статическим членам и вложенным типам которого можно получить, не указывая имя типа. Пример:

```C#
using static System.Console;
using static System.Math;

WriteLine(Sqrt(3*3 + 4*4));
```

После компиляции превращается в конструкцию try { //code } finally { obj.Dispose() }

#### Есть ли деструкторы в C#? Если есть, то что они из себя представляют?

В C# есть есть как управляемые объекты, которые может запросто очистить сборщик мусора, так и неуправляемые, которые не могут контролироваться Garbage Collector, точнее он просто не знает как их очищать (обращение к API нашей ОС или работа с сетью). C# предоставляет 2 метода, позволяющих собственноручно указать, что нужно сделать, перед удалением экземпляра класса. И это: Dispose (от IDisposable) и Finalize. Через IDisposable - просто реализация интерфейса, а Finalize - определение деструктора в классе, который после компиляции преобразуется в метод Finalize от базового класса `object`.

Так выглядит деструктор класса `MyStream`:

```C#
// destructor
~MyStream(){
	// finalize action
}
```

После компиляции получится следующая конструкция:

```C#
protected override void Finalize() 
{
	try
    {
        // destructor code
    } 
    finally
    {
        base.Finalize();
    }
}
```

И про метод Dispose. Компилируется конструкция using грубо говоря в такой код:

```C#
try 
{
    var stream = new MyStream();
    stream.WriteIntoFile("TEXT");
} 
finally
{
	stream.Dispose();
}
```

Однако безопаснее Disposable объект вызывать в конструкции using, поскольку это гарантирует нам, что даже в случае исключения ресурсы будут высвобождены.

А лучше вообще комбинировать подходы. Но [правильно](https://metanit.com/sharp/tutorial/8.2.php).

**Что такое Garbage Collector (сборщик мусора)?**

GC - инструмент CLR. Высвобождает из памяти объекты - удаляет из стека:

* ссылки на объекты reference type
* value type объекты

Сборщик мусора не запускается сразу после удаления из стека ссылки на объект, размещенный в куче. Он запускается в то время, когда среда CLR обнаружит в этом потребность, например, когда программе требуется дополнительная память.

Всего разделяют 3 поколения объектов:

* 0-е - новые объекты, которые еще ни разу не подвергались сборке мусора
* 1-е - объекты, которые пережили одну сборку
* 2-е - объекты, прошедшие более одной сборки мусора

#### Что такое ООП? Какие есть парадигмы. Кратко о каждой

Кста, ООП нафантазировал у себя в голове Алан Кей. Ну так, чиста для справки.

*Объект* - базовая единица объектно ориентированной системы.
*Посылка сообщений* - единственный способ обмена информацией между обектами.
Каждый объект принажлежит определенному классу.
*Поведение* объекта определяется его классом. 
Классы наследуют функциональность от предка.

1. **Инкапсуляция** - обеспечивает сокрытие, но не является им.
    Инкапсуляция (encapsulation) - это механизм, который объединяет данные и код, манипулирующий зтими данными, а также защищает и то, и другое от внешнего вмешательства или неправильного использования. Можно представить инкапсуляцию как защитную оболочку, которая предохраняет код и данные от произвольного доступа из других кодов, определённых вне этой оболочки. Характерной является ситуация, когда открытая часть объекта используется для того, чтобы обеспечить контролируемый интерфейс закрытых элементов объекта.

2. **Наследование** - это механизм языка, который позволяет описывать новый класс на основе существующего

   Сама по себе парадигма, ну так себе, так как дает возможность создать вереницу из сотни наследников от базового класса, внесение изменений в который может увинчаться пинком под зад от коллег по цеху. На такой случай не забываем про модификатор `sealed`. Но вообще, данная парадигма существует, по большому счету, ради полиморфизма.

3. **Полиморфизм** - это способность обьекта использовать методы производного класса.
Полиморфизм обеспечивает инкапсуляцию и имеет 3 формы:
* **Ad-Hoc** - возможность перегрузки методов.

```C#
public void WriteMessage()
{
	Console.WriteLine("Paste your text");
}
public void WriteMessage(string text)
{
	Console.WriteLine(text);
}
```

* **Параметрический** - классы Generic.

* **Полиморфизм подтипов** - механизм наследования (переопределяем виртуальный метод) и апкаст (B : A; A field = new B()).

```C#
public virtual void WriteMessage(string text) // класс A
{
	Console.WriteLine(text);
}
public override void WriteMessage(string text) // класс-наследник B
{
	Logger.Log(text);
	base.WriteMessage(text);
}

public void Main()
{
	A a = new A();
	A b = new B(); // upcast
	a.WriteMessage("Я хочу пиццу"); // тупа в консоль
	b.WriteMessage("Я хочу коллу"); // тупа в консоль и в лог-файл
}
```

Полиморфизм также обеспечивает сокрытие
```C#
public class B : A
{
	public void Test2() => Console.Log("Test2");
}
public void Main()
{
	A a = new A(); // типа в нем метод Test
	A b = new B();
	a.Test(); // Test
	b.Test2(); // после апкаста нет возможности скомпилить
}
```

4. **Абстракция** - задача заключается в том, чтобы выделить минимальные методы и поля класса для возможности решения задачи.

5. **Отправка сообщений** - способ сообщения между объектами. По факту, использование методов одного объекта другим.

```C#
// ко-ко-ко, мы так тактично выделели такую реализацию в целую парадигму, ко-ко-ко
public void Call() // класс One
{
	Logger.Log("Message");
} 
...
private One _one;
public void Foo() // класс Caller
{
	one.Call();
}
```

#### Что из себя представляют ключевые слова ref и out?

**ref** - ключевое слово, которое можно использовать для передачи ссылки на объект не только ссылочного типа, но и значимого.

**out** - ключевое слово, схожее по смыслу с ref. Передает ссылку на объект. При использовании в качестве возвращаемого значения в аргументе, нужно проинициализировать базовым значением.

#### Ну как там с паттернами?

Всего 3 категории базовых паттернов.

* **Порождающие** - беспокоятся о гибком создании объектов без внесения в программу лишних зависимостей (фабричный метод, абстрактная фабрика, строитель, прототип, одиночка и др.)
* **Структурные** - показывают различные способы построения связей между объектами (адаптер, прокси, декоратор и др.)
* **Поведенческие** - заботятся об эффективной коммуникации между объектами (команда, итератор, снимок - Memento, наблюдатель, состояние и др.)

**Архитектурные паттерны приложений**: MVC, MVVM

*Вытекающие вопросы*: рассказать про MVC и MVVM

#### Что за интерфейсы такие IEnumerable, IQueryable?

IEnumerable - Предоставляет перечислитель (IEnumerator), который поддерживает простой перебор элементов коллекции.

IQueryable - плохо знаком, но используется в EF для генерации SQL запроса через LINQ.

#### Что можно перебрать в цикле foreach?

Либо класс, реализующий интерфейс IEnumerable, либо просто класс, который может нам вернуть класс (якобы енумератор), в котором есть свойства Current и метод MoveNext. Даже наследование от интерфейсов будет излишнее.

#### Для чего используется ключевое слово yield?

(подробную инфу лучше расписать, так как на момент 05.02.22 я это отлично понимаю) Кратко, для перебора коллекций и оптимизации при итерации по этой коллекции.

#### string против StringBuilder?

String - это ссылочный тип, имеющий семантику значимого типа. То-есть, на этапе выполнения программы нам не известен точный размер значения строки, в отличии от других значимых типов, поэтому помещается она в куче. Что значит имеет семантику значимого типа?

```C#
string a = "sample";
string b = "sample";
if(a == b)
{
	Console.Log("a equals b");
}
```

По логике вещей, у нас не выведется лог в консоль, поскольку в if будет значение false. Это если судить, что два ссылочных типа сравниваются по ссылке. Однако реальной логике это никак не соответсвует, подумали ребята из мелкомягких и бахнули нам семантику значимых типов - сравнение по значению.

> Семантика — раздел лингвистики, изучающий смысловое значение единиц языка. 

Как это получилось сделать? Для равенства и неравенства был переопределен оператор сравнения (== и !=).

**Иммутабельность строк.** Иммутабельность - неизменяемость.

```C#
string hello = "Hello world!";
string newString = hello.Remove("world!"); // переменная hello не изменилась, тк была создана новая строка
```

Получается так, что любые операции над строками возвращают нам новую строку, не изменяя старую.

**Оптимизация операций над строками.** 

```C#
string a = "ABC"; // +
string b = "DCA"; // +
string c = "DCA"; // -
string result = a + " " + b + " " + c;
// в памяти будет создано 4 строки. повторяющиеся строки не будут повторно создаватся
```

Под капотом у StringBuilder происходит конвертация строки в массив char и все преобразования происходят с этим массивом.

#### Приведение типов? Как можно выполнить?

Разделяют:

* Явные преобразования

  ```C#
  string input = Console.ReadLine();
  int num = (int)input;
  // ...
  Rat rat = new Rat();
  Animal animal = (Animal)rat;
  ```


* Неявные преобразования

  ```C#
  int a = 141234241;
  long b = a;
  // ...
  Child child = new Child();
  Parent parent = child;
  ```

* Пользовательские преобразования

```C#
public static implicit operator byte(Digit d) => d.digit;
public static explicit operator Digit(byte b) => new Digit(b);
```
* Преобразования с использованием вспомогательных классов

```C#
var num = Convert.ToInt32(Console.ReadKey());
if(int.TryParse(Console.ReadKey(), out var number))
{
	num += number;
}
```

* Использование ключевых слов

```C#
if(rat is Animal animal)
{
     Console.Log("rat is animal");
}
// ...
var animal = new Rat() as Animal;
```

#### У массивов есть методы Copy и CopyTo. Их разница?

`Copy` делает поверхностное копирование, а `CopyTo` глубокое.

#### Разница между интерфейсами и абстрактными классами?

1. Интерфейсы не могут задавать конструкторы
2. Интерфейсы задают действия объекту, а абстрактный класс определяет основные свойства и минимальный рабочий функционал. (прим. Pet от абстр. класса Animal, а метод Run от IRunable)

#### (НЕТ ЗАПИСЕЙ)Наследование и композиция, агрегация. Когда, что лучше использовать?

#### (НЕТ ЗАПИСЕЙ)Что такое Task?

Task - это абстракция вокруг потока

#### Что такое stackalloc и что можно о нем сказать?

Выражение `stackalloc` выделяет блок памяти в стеке. Выделенный в стеке блок памяти, который создает этот метод, автоматически удаляется по завершении выполнения метода. Вы не можете явным образом освободить память, выделенную `stackalloc`. Выделенный в стеке блок памяти не подвергается [сборке мусора](https://docs.microsoft.com/ru-ru/dotnet/standard/garbage-collection/), поэтому его не нужно закреплять с помощью [инструкции ](https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/keywords/fixed-statement).

```C#
int length = 3;
Span<int> numbers = stackalloc int[length];
for (var i = 0; i < length; i++)
{
    numbers[i] = i;
}
```

Как известно, в stack'е кол-во памяти ограничено. Поэтому переусердствовать с "оптимизациями" тоже не стоит

```C#
const int MaxStackLimit = 1024;
Span<byte> buffer = inputLength <= MaxStackLimit ? stackalloc byte[MaxStackLimit] : new byte[inputLength];
```

Примечание:

* Старайтесь не использовать `stackalloc` в циклах. Выделяйте блок памяти за пределами цикла и используйте его повторно внутри цикла.

#### (НЕТ ЗАПИСЕЙ)Что такое Span и как используется?

#### Что такое явная и неявная реализация интерфейса? 

При **явной** реализации мы можем обращаться к реализуемому классом `Foo` методу `Foo` только при приведении к общему интерфейсу `IFoo`. При **неявной** реализации, что случается на практике чаще, мы можем обращаться к методу `Foo` в классе `Foo` при любых условиях.

```C#
internal class Program
{
    static void Main()
    {
        IFoo fooEx = new Foo(); // explicit - явная
        fooEx.Foos();
        Foo fooIm = new Foo(); // implicit - неявная
        fooIm.Foos(); // no access
    }
}

public interface IFoo
{
    void Foos();
}

public class Foo : IFoo
{
    void IFoo.Foos()
    {
        Console.WriteLine("Явная");
    }

    public void Foos()
    {
        Console.WriteLine("Неявная");
    }
}
```

Такое разделение подходов может использоваться для сокрытия реализации некоторых специфичных методов, например, для классов, реализующих `IEnumerable`, можно явно реализовать методы `GetEnumerator()`, а для класса, реализовавшего интерфейс `ISerializable` - метод `Serialize()`. А **можем использовать сразу явную и неявную**, результат выполнения которых может отличаться!

#### Методы и классы с ключевым словом `sealed` (в плане оптимизации)

JIT компилятор в процессе выполнения программы достает IL-код из информации о методе в метаданных, после чего компилирует в машинный код и скидывает в память, после чего использует готовый код, который не требует постоянной перекомпиляции. В момент обращения JIT к метаданным, он ищет метод или класс, а также заодно просматривает его наследников и всех, кто его переопределяет/дополняет. Таким образом, чтобы избавить его лишних действий можно запечатать методы и классы, что будет означать, что дальнейшее углубление по иерархии наследования не является нужным.

## Разбор части 3 - специфика направления - ASP.NET Core

#### Жизненный цикл запроса asp.net запросов / сервисов?

Три жизненных цикла:

* **Singleton** - создается один раз на весь work-time (не стоит забывать про thread-safe)
* **Scoped** - создается на каждый запрос
* **Transient** - создается каждый раз при вызове, даже в рамках одного запроса 



## Junior+ / Middle

#### Для чего нужен HTTP и как он устроен?

**HTTP - Hyper Text Transport Protocol** - протокол, используемый для передачи HT-документов с ссылками на другие документы. 

Стартовая строка запроса: *method URI HTTP/ver.*

```http
GET /request_uri HTTP/1.1
```

В данном примере мы обращаемся к ресурсу /request_uri по методу `GET`, используя `HTTP` протокол версии `1.1`. 

**URI - Uniform Resource Identifier** - путь до ресурса.

URI не обязателен в HTTP запросе и может не использоваться. На его месте может стоять *астериск* (*). Например, используя метод `Options`

```http
OPTIONS * HTTP/1.1
```

`Options`, кстати, можно было бы использовать для получения методов в заголовке `Allow: ...`, то-есть можно ли обращаться к данному ресурсу через Get, Post или как то еще. Но из-за сложности бизнес логики (требования к авторизации, ограничении прав доступа и прочего) лепить Options на каждый адрес нереально геморно и не практично, поэтому хуета, но идея прикольная.

Далее необходим заголовок `HOSTЖ address`, указывающий, куда именно мы хотим обратиться.

```http
GET /index.html HTTP/1.1
HOST: google.com
```

> **Уточнение**. Для переноса строки используется carriage-return (CR), то-есть "\r", а не linefeed (LF) "\n". Каждая ОС считывает свой символ для переноса строки, поэтому используя не самый модный редактор в разных ОС можно получать "кракозябры".
>
> Однако на стороне сервера можно настроить использование CR or LF

Ответ от HTTP-сервера

Ответ отправляется по такому паттерну: *HTTP/ver status_code additional_message*

```http
HTTP/1.1 200 Success-Response
```

*additional_message* - это сообщение readable для пользователя, в котором можно указать уточнения по поводу обработанного запроса (не использует CR or LF).

Так выглядит минимальное сообщение по протоколу HTTP. Далее указываются дополнительные заголовки, по типу:

```http
GET / HTTP/1.1
HOST: example.org
Server: nginx/1.4.88
Content-Length: 12 

Hello World!
```

Тело ответа следует через два переноса строки после последнего заголовка. Вот и все, пока что.

#### В чем разница между HTTP и HTTPS? Как устроен HTTPS?

**HTTPS - Hypertext Transport  Protocol Secure** - короче, HTTP с шифрованием. Это шифрование могут обеспечивать другие протоколы, например, **SSL, TLS**. <u>HTTPS - это обертка</u> (расширение) над ранее указанными протоколами, использующие шифрование данных. Также есть разные модификации HTTPS по типу SPDY (speedy). Эта модернизация уменьшает время задержек за счет сжатия данных, а также поддерживает мультиплексирование дополнительных ресурсов для передачи в рамках одного соединения. Но сейчас не об этом.

Когда пользователь отправляет запрос на свой любимый сайт, его данные проходят через множество сетей, каждая из которых может прослушиваться, либо быть не защищенной. Функция шифрования данных не доступна по умолчанию по некоторым причинам

* Это дополнительные вычислительные мощности
* Размер пакетов увеличивается
* Сходит на нет возможность кеширования (под вопросом)

Для разбора тонкостей, окунемся в мир криптографии хотя бы на общем уровне. 

**Transport Layer Security (TLS)** - наследник SSL - протокол, применяемый для более безопасного HTTP соединения. TLS находится на уровень ниже в модели [OSI](https://ru.wikipedia.org/wiki/%D0%A1%D0%B5%D1%82%D0%B5%D0%B2%D0%B0%D1%8F_%D0%BC%D0%BE%D0%B4%D0%B5%D0%BB%D1%8C_OSI), это значит, что все операции сначала выполняются с TLS, а только потом в работу вступает HTTP. TLS является гибридной криптографической системой, то-есть использует *симметричное* и *асимметричное* шифрование. Пропущу тему с криптографией, поскольку расписывать все это говно я не хочу, НО! 

![](./imgs/ModelOSI.png)

![](./imgs/TLS-SSL.png)

При обмене публичными ключами между клиентом и сервером может возникнуть вопрос "А точно ли я общаюсь / обмениваюсь ключами с сервером, а не с кем то другим?". Для этого люди придумали ***сертификаты***. Сертификат - это такой файл, использующий цифровую подпись, которая утверждает, что данный публичный ключ связан именно с этим доменом, то-есть 100% вы общаетесь с кем и хотели. И такие сертификаты, конечно же, стоят денег, ведь я их выдачи нужно убедиться, что

* Вы имеете доступ к домену вашего сайта
* Вы существуете

#### Что такое чистая функция?

Читая дядюшку Боба можно без раздумий ответить на этот вопрос, однако не умные юзеры могут даже не знать кто это такой. Поэтому вкратце для всех:

* Это функция, которая на одни входные данные возвращает всегда одни и те же значения
* Не имеет побочных эффектов 

#### Что такое Dependency Injection и как оно устроено?

**Inversion of Control (инверсия управления)** — это некий абстрактный принцип, набор рекомендаций для написания слабо связанного кода. Суть которого в том, что каждый компонент системы должен быть как можно более изолированным от других, не полагаясь в своей работе на детали конкретной реализации других компонентов.

**Dependency Injection (внедрение зависимостей)** — это одна из реализаций этого принципа (помимо этого есть еще [Factory Method](http://en.wikipedia.org/wiki/Factory_pattern), [Service Locator](http://en.wikipedia.org/wiki/Service_locator_pattern)).

**IoC-контейнер** — это какая-то библиотека, фреймворк, программа если хотите, которая позволит вам упростить и автоматизировать написание кода с использованием данного подхода на столько, на сколько это возможно.

#### SaaS, Paas, IaaS - что это?

Стек облачных технологий состоит из трех частей, каждая из которых представляет отдельную категорию сервисов ([ресурс](https://www.youtube.com/watch?v=qJ5pBb2i8j4)):

- SaaS — приложения, работающие в облаке, доступ к которым конечные пользователи получают через веб.
- PaaS — набор инструментов и сервисов, облегчающих разработку и развертывание облачных приложений.
- IaaS — вычислительная инфраструктура (серверы, хранилища данных, сети, операционные системы), которая предоставляется клиентам для разворачивания и запуска собственных программных решений.

![](./imgs/CloudInfrastructure.jpg)

![](./imgs/AsAServiceExample.png)
