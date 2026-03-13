
# Reactor Game
*(this repository is archive, actual build you can download on [itch.io](https://tgetr.itch.io/reactoric#download))*

**Версии до Alpha 0.3.2-S собраны на небезопасных версиях движка, используйте их на свой страх и риск.**

**Reactoric** --- мой первый полноценный независимый проект, выпущенный на Itch, основная суть игры заключается в управлении реактором (сильно упрощенном, скорее только его стержнями). 	В игре присутствуют интересные механики, такие как:

 - Управление стержнями реактора (главная механика)
 ![](<Assets/[DOCS] Images/[SCR] Reactor.png>)
 - **Задания (Выбираются случайно, из заготовленного пула)**
 ![](<Assets/[DOCS] Images/[SCR] Task.png>)
 - **Усталость (Нарастает по мере смены, при 12-часовых сменах почти всегда кончается раньше времени и вызывает дебафы)**
 ![](<Assets/[DOCS] Images/[SCR] FatigueZero.png>) ![](<Assets/[DOCS] Images/[SCR] FatigueHalf.png>) ![](<Assets/[DOCS] Images/[SCR] FatigueFull.png>)
 *(Шкала усталости. Зелёное - остаток энергии)*
 ![](<Assets/[DOCS] Images/[SCR] DarkFullScreen.png>)
 *Эффект затемнения, возникает когда энергия кончается*
 
 - **Температура и энерговыработка (Цели, то из-за чего игра может кончится)**
 - **Часы времени и длины смены. Смена может идти от 4 до 12 часов, длительность влияет на усталость и задания.**
 - **Лог последних событий. Экран (объект) с текстом  в котором отображаются последние события, такие как превыщение нормы по температуре или слишком маленькая энерговыработка.**
 - Звуковое оповещение при превыщении норм.
### Технические особенности
Стек технологий: 
 - **Unity**: 2022.3.27.61434 (Early) -> 6000.0.30f1 (Main) -> 6000.2.7f2 (Security)
 - **C# (.NET):** C# 12 (.NET 8) -> C# 13 (.NET 9) 
 - Unity Localization Package
### Архитектурные решения:
**Независимый игровой цикл (Core Loop):**
Логика реактора (`ReactorController`) вынесена в отдельную корутину, которая обновляет состояние каждые 0.75 секунд. Это отвязывает физику нагрева и выработки энергии от частоты кадров (FPS), делая игру стабильной на слабых устройствах.

    void  Start()
    {
    StartCoroutine(ControlLoop());
    }
    
    IEnumerator  ControlLoop()
    {
    while (true)
    {
    UpdateReactorState();
    yield  return  new  WaitForSeconds(0.75f);
    }
    }
    
    ...
    
    void  UpdateReactorState()
    {
    _activeRods  =  _rodsController.ActiveRodsCount;
    ApplyTemperatureChange();
    ApplyEnergyChange();
    UpdateAlarms();
    }
 **Математическая модель:** 
Для расчета температуры и энергии используются функции линейной интерполяции (`Mathf.Lerp` и `Mathf.InverseLerp`). Это позволяет динамически масштабировать множители в зависимости от точного количества активных стержней, создавая плавную кривую сложности.

    void ApplyTemperatureChange()
    {
    float multiplier = GetDynamicMultiplier(_activeRods, 6, 30, 0.25f, -0.2f);
    float delta = Temperature * multiplier;

    if (Mathf.Approximately(Temperature, 0f))
        delta = 0.1f;
        
    Temperature += delta;
    }

    ...
    
    float GetDynamicMultiplier(int value, int min, int max, float minMult, float maxMult)
    {
        float t = Mathf.InverseLerp(min, max, value);
        return Mathf.Lerp(minMult, maxMult, t);
    }
   **Управление временем и событиями:** Игровая сессия, усталость и генерация заданий управляются через асинхронные вызовы `InvokeRepeating`, что позволяет гибко настраивать скорость течения внутриигрового времени независимо от реального.
   
    void Start()
    {
        _image = GameObject.Find("Image").GetComponent<Image>();
        InvokeRepeating("CheckDark",0,5);
    }

    ...

    void CheckDark()
    {
        if(FatigueIndex <=0)
        {
            _anim.SetTrigger("Dark");
        }
    }

   **Сохранение состояний:** Реализована персистентность данных. Состояние каждого отдельного стержня реактора (`RodsController`) и глобальная статистика (`GlobalData`) сохраняются между сессиями с помощью `PlayerPrefs`.
      
    private void OnApplicationQuit()
    {
        SaveToPrefs();
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetInt("Day", Day);
        PlayerPrefs.SetInt("ReactorStatus", ReactorStatus);
        PlayerPrefs.SetInt("Heat", Heat);
        PlayerPrefs.SetInt("Cold", Cold);
        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.Save();
    }
   **CI/CD & Безопасность:** Настроен автоматический статический анализ кода (SAST) с использованием **GitHub CodeQL** для отслеживания уязвимостей и контроля качества архитектуры.
