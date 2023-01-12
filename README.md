# Clicker-Test-Task
Test task project

### Архитектура
В задании не использовал никаких сторонних библиотек, так как это не было обговорено. 
Инициализация всего происходит с инсталлера, который висит на окне бизнесов.
Он создает все инфраструктурные сервисы, сервис(контроллер) главного окна, презентер главного окна. 
Сервис главного окна агрегирует сервисы для каждого бизнеса. 
Так же и с презентером главного окна, который агрегирует их презентеры.
В конструкторе презентеры связывают модли с вью, начинается геймплейный цикл.

### MVP
Агрегация была выбрана для линейности порядка инициализации. с Zenject-ом, я бы наверное не стал так делать. 
MVP было выбрано потому, что я считаю это самым удачным способом подключения UI в Unity, если не брать сторонние решения. 
Я отдаю предпочтение варианту, когда презентер знает и модели и вью. Модель и вью друг друга не знают.

### Сохранение
Сохранение реализовано как отдельным сервисом, так и интерфейсом, который поулчает колбек о закрытии приложения.
Было намерено использовано оба варианта, для демонстрации. Хотя обычно сейв решается сторонним решением, по моему опыту.

### Конфиги
Конфиги разработаны для бизнесов. Все названия я не стал выносить в отдельный конфиг, посчитал логичнее все держать в одном.
Был сделан конфиг с форматом вывода текстовых значений. Раньше такого не делал, просто как то пришла идея и решил посмотреть, как оно смотрится и работается)

### Что бы я исправил
1. Нужна фабрика для окон бизнеса
2. Нет инфраструктуры проекта (инит сцены с прогревом сервисов и прочего)
3. Очень большие конструкторы у сервисов окон. Сервис бизнеса следует раздробить на сервис бизнеса и агрегируемый им сервис апгрейдов.
4. Таймер, сейв сделаны чисто для демонстрации, там очевидно нужны разные доработки, но это и так понятные мелочи.
5. Возможно првитая особенность называть контроллеры сервисами будет путать)
