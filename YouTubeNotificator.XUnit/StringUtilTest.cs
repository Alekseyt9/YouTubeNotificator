

using Xunit;
using YouTubeNotificator.Domain.Helpers;

namespace YouTubeNotificator.XUnit
{
    public class StringUtilTest
    {
        [Fact]
        public void SplitStringTest()
        {
            var msg = @"
<b>Максим Кац</b>
    • [15:27 21.01.2023] <a href='https://www.youtube.com/watch?v=oazmN083FzA'>Блокада Ленинграда | История Второй мировой (English subtitles) @Max_Katz</a>
    • [14:26 20.01.2023] <a href='https://www.youtube.com/watch?v=T8gd9tKxYGk'>Бахмут как Первая мировая | Путину нужны новые люди на фронте (English subtitles) @Max_Katz</a>

<b>General SVR</b>
    • [18:05 20.01.2023] <a href='https://www.youtube.com/watch?v=PvseIyZgAIU'>Ответы на вопросы 20.01.2023</a>
    • [20:33 19.01.2023] <a href='https://www.youtube.com/watch?v=392HP1aQWEY'>Путин продолжает звонить Раиси. Ошибка Небензи. Купание красного Коня.</a>

<b>Олег Жданов</b>
    • [18:10 21.01.2023] <a href='https://www.youtube.com/watch?v=80As8pRLcuI'>21.01 Оперативная обстановка. Возможности ВСУ благодаря поставкам вооружений. @OlegZhdanov</a>
    • [14:29 21.01.2023] <a href='https://www.youtube.com/watch?v=aeXHVmpwqHQ'>Что привёз глава ЦРУ в Киев, как США помогут освободить Крым и почему в Москву свозят ПВО? — Жданов</a>
    • [14:29 21.01.2023] <a href='https://www.youtube.com/watch?v=JgAcbPvutow'>ЖДАНОВ У Москві лунатимуть вибухи : Велика оборонна операція ЗСУ: Хід війни зміниться@OlegZhdanov</a>
    • [19:50 20.01.2023] <a href='https://www.youtube.com/watch?v=AyW3jVZsq4w'>20.01 Оперативная обстановка. Рамштайн 8. @OlegZhdanov​</a>
    • [22:11 19.01.2023] <a href='https://www.youtube.com/watch?v=81LmwuEIOo8'>ЖДАНОВ путин готовит МАСШТАБНУЮ АТАКУ   ИЗВЕСТНА ДАТА! Москва ТОТАЛЬНАЯ мобилизация</a>
    • [21:26 19.01.2023] <a href='https://www.youtube.com/watch?v=fySlpzG2acQ'>19.01 Оперативная обстановка. Вероятное направление наступления войск рф. @OlegZhdanov</a>

<b>HighLoad Channel</b>
    • [18:06 19.01.2023] <a href='https://www.youtube.com/watch?v=zYKv7tr64tU'>Как ментально полюбить и начать писать тесты на примере Yii 2 и Codeception / Артем Волторнистый</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=0q9UyUlYPos'>Обзор архитектуры быстрого сборщика логов на Go / Владимир Витковский (Ozon)</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=4h73gMfdy8c'>Проксирование данных для Hadoop / Андрей Ильин (Сбер)</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=MFPpaMIgcCU'>Идеальный шторм: когда готовил систему к росту нагрузки, но не успел / Алексей Мерсон</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=T-JnDK6KyxI'>API Gateway - как не допустить хаоса при цифровой трансформации / Алексей Коновкин (Nexign)</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=uLiMIrja25w'>Найди свой Vector в построении высоконагруженной системы логирования / Илья Вазем (СберМегаМаркет)</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=ve2Ft4yceYc'>Резервирование маршрутизаторов в дата-центре / Кирилл Малеванов (Selectel)</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=BUBPYP4lm80'>Строим Security Сenter для Kubernetes-платформы / Алексей Миртов (Yandex Cloud)</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=ITuDX5VrzBo'>Закрытие HighLoad++ Foundation 2022</a>
    • [18:03 19.01.2023] <a href='https://www.youtube.com/watch?v=aB7_940m3_s'>Визуальное проектирование масштабируемых приложений / Максим Цепков (CUSTIS)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=kN1-0yHZRVc'>Инструменты BI в платежной системе «Мир» / Никита Смирнов (Мир Plat.Form)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=A6w_7QZGgrg'>Как работать с секретами в Golang, чтобы минимизировать хаос / Сергей Киммель (Quadcode)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=LmPomWwNXsc'>Избавляемся от кэш-промахов в коде для x86-64 / Евгений Буевич (RU-CENTER)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=NPwmk_QLNxQ'>Какие «пепелацы» мы изобрели между DWH и BI? / Алексей Пахомов (Sportmaster Lab)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=NtAn3Sazdrk'>Опыт MY.GAMES Cloud по обработке потерь и построению инфраструктуры / Алексей Лыков (MY.GAMES Cloud)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=aaJcSzDoZr4'>Особенности построения аналитического in-house data lake в EdTech / Валентин Пановский (ex-Skillbox)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=x7t4z78bXVg'>Client as Service, или Наш опыт в разработке платформы для экспериментов / Артем Селезнев (Магнит)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=zOTDQT-6Sd4'>Круглый стол ""Реальный cектор"": ""Почему разработчикам интересно работать в индустриальных компаниях""</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=0Hl3aEcs0iQ'>Безопасность цепочки поставки Open Source-компонентов / Алексей Смирнов (profiscope.io)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=1e4JwZX4OfU'>С++ и msgpack: проектирование кастомных протоколов / Александр Ляпунов (VK)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=2mj9r6-xC9E'>IT-экосистема СИБУРа / Сергей Романков (СИБУР Диджитал)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=6uqabt_WfeQ'>Российский балансировщик нагрузки / Павел Иващенко (NFWare)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=7ShA_LQ4XRw'>Как автоматизировать разбор проблем в дебаггере / Сергей Козлов (Лаборатория Касперского)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=8RUnnXA1eHA'>Особое мнение: предугадываем фрод без дата-сайнса / Александр Сальков (Sportmaster Lab)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=JicdzowJHcU'>Как строить больше ML-пулов на MapReduce, а дежурить меньше / Илария Белова, Никита Путинцев(Яндекс)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=LkcAtzdugSs'>1С:Предприятие — low-code-платформа для цифровизации бизнеса / Никита Старичков (1С)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=SScbFrDcooo'>Построение цифровой архитектуры / Алексей Фельде (Магнит)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=UK0Ct8xApZw'>DevSecOps там, где была только электронная почта / Олег Кондрашин (СИБУР Диджитал)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=WTDrfLOnMKI'>Istio Service Mesh в федеративных топологиях Kubernetes / Максим Чудновский (Сбертех)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=WqJ3_rqJCh8'>Как работает С++-движок рекламного сервера, с какими проблемами мы сталкиваемся/Дмитрий Корчагин(VK)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=YST24EiPGnc'>Архитектура Vitastor. Тёмная сторона моей распределённой СХД / Виталий Филиппов (Личный проект)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=aYuhBVBPZ2c'>Аналитика в Sportmaster Lab, зачем нам Clickhouse и Tableau? / Алексей Пахомов (Sportmaster Lab)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=cJyNZXY0-ug'>Оптимизация инференса нейронок на CPU / Анастасия Торунова (Тинькофф)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=iDFBWczQVcg'>Performance as a service: делаем быстрее и дешевле через сервисный подход / Кирилл Юрков (Самокат)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=iFJtpZZlGMg'>Почему никто не умеет делать экспертные системы в промышленности / Андрей Зубков (ЕВРАЗ)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=ir20yy8B4-w'>Что творится в агротехе / Евгений Савин (Green Growth)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=q4-q8ZeSdss'>Как цифровая трансформация приводит к росту числа ML-моделей в проде / Денис Занков (Газпромбанк)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=uZq0rwWrKQA'>РБК и неожиданный хайлоад / Анна Абрамова (РБК)</a>
    • [18:01 19.01.2023] <a href='https://www.youtube.com/watch?v=y-oZhlk-Z4w'>Управление доступом к операционным системам на серверах / Дмитрий Корняков (Мир Plat.Form)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=0M0gDXBauD4'>Как мы готовили поиск в Delivery Club / Иван Максимов (Delivery Club), Евгений Исупов</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=229RE8fwMNs'>Как мы ускорили Яндекс Go на несколько секунд / Денис Исаев (Яндекс Go)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=J7IePhAZbCA'>Построение HPC/GPU-кластеров для машинного обучения / Дмитрий Монахов (Яндекс)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=NXU7zULI-lA'>Трансформеры в Такси: в нужное время — в нужном месте!/Артем Просветов, Эрнест Глухов (Яндекс Такси)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=T-wzoCJoF3s'>Нейросетевые рекомендации сообществ ВКонтакте / Любовь Пекша (ВКонтакте)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=Yf48-6Ce3_M'>ML для ML в задачах качества данных / Денис Занков (Газпромбанк)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=dd_LGH_261E'>Генерация хайлайтов / Евгений Россинский (ivi)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=gB0mrl1KXfU'>Облегчение моделей виртуальных ассистентов Салют/Ибрагим Бадертдинов ,Александр Абрамов(SberDevices)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=k2ccFXWdBN4'>YDB: мультиверсионность в распределенной базе / Андрей Фомичев (Яндекс)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=nc0FfVwmjBU'>Exactly once-передача данных без материализации / Юрий Печатнов (Яндекс)</a>
    • [17:59 19.01.2023] <a href='https://www.youtube.com/watch?v=sXkTGrmsOkk'>Как строить Low Latency-рекомендательный трансформер на миллион RPS / Всеволод Светлов (Яндекс)</a>

<b>Валерий Соловей</b>
    • [13:00 22.01.2023] <a href='https://www.youtube.com/watch?v=AjiTWx8Dufk'>Черный и белый Путин. В ""Живом гвозде"" с Лизой Аникиной и Лизой Лазерсон. 18+. Запись 21.01.</a>
    • [13:00 20.01.2023] <a href='https://www.youtube.com/watch?v=ScKX5x-rDrQ'>Стрим Валерия Соловья: ответы на вопросы. Трансляция 19 января. 18+</a>
    • [21:30 19.01.2023] <a href='https://www.youtube.com/watch?v=fVfYw6tY2ts'>Стрим Валерия Соловья: ответы на вопросы</a>

<b>MaxCapital</b>
    • [14:14 21.01.2023] <a href='https://www.youtube.com/watch?v=W8NjN9SsUvE'>Россия идёт по пути Ирана #shorts</a>
    • [16:13 20.01.2023] <a href='https://www.youtube.com/watch?v=H3duXy15JvY'>Инвестиции или депозит – что выгоднее? Выбираем оптимальный вариант</a>
    • [17:24 19.01.2023] <a href='https://www.youtube.com/watch?v=iUzBdQV-IlI'>❗Профит закончится через год #shorts</a>

<b>ФЕЙГИН LIVE</b>
    • [0:12 22.01.2023] <a href='https://www.youtube.com/watch?v=2vXssPoueYA'>День триста тридцать второй. Беседа с @arestovych Алексей Арестович</a>
    • [0:14 21.01.2023] <a href='https://www.youtube.com/watch?v=8fVbkr5qaRM'>ОРУЖИЕ ДЛЯ УКРАИНЫ. Беседа с Андреем Илларионовым</a>
    • [23:13 20.01.2023] <a href='https://www.youtube.com/watch?v=ufKZRqTq7_M'>УТИЛИЗАТОР. Беседа с Владимиром Осечкиным  @MrGulagunet</a>
    • [23:54 19.01.2023] <a href='https://www.youtube.com/watch?v=PujVAg02qd4'>День триста тридцатый. Беседа с@arestovych Алексей Арестович</a>
    • [23:04 19.01.2023] <a href='https://www.youtube.com/watch?v=pKJj3w86BLk'>РУССКАЯ РАКЕТА В ДНЕПРЕ. Беседа с @yulialatynina71 Юлия Латынина</a>

<b>Демидков. Россия глазами современника истории.</b>
    • [20:21 19.01.2023] <a href='https://www.youtube.com/watch?v=UC41k7hshI4'>Мобилизации не случилось и 18 января, как многие предвещали. Почему? Чего ждать?</a>
";

            var res = StringUtils.SplitString(msg, 4096);
        }


    }
}
