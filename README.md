# CRUD
Простой многоуровневое приложение для создания/редактирования/удаления заказов.

> Строка подключения к базе данных устанавливается в Infrastructure > DependencyInjection.cs

## База данных
- Orders
   - Id [int] [pk]
   - Number [int]
   - Date [datetime2(7)]
   - ProviderID [int]
- OrderItems
   - Id [int] [pk]
   - OrderId [int]
   - Name [nvarchar(max)]
   - Quantity [decimal(18,3)]
   - Unit [nvarchar(max)]
- Providers
   - Id [int] [pk]
   - Name [nvarchar(max)]
   
## Структура приложения
- Главная страница [Dashboard]
   - Фильтр для данных
   > фильтрация происходит по:\
   > Order.Number, Order.Date (по ограниченному отрезку), Order.ProviderId,\
   > OrderItem.Name, OrderItem.Unit,\
   > Provider.Name (Используется Order.ProviderId)
   - Таблица с данными Orders
   - Кнопка создания нового заказа
   - Форма просмотра заказа (по клику на строку в таблице)
   > Внутри формы кнопки удаления, редактирования\
   > Отражена вся полезная для пользователя информация о заказе
- Страница добавления/обновления заказа [Order]
   - Все поля для редактирования
   - Кнопка добавления новых строк в заказ
   - Все строки в заказе (OrderItem) представлены в виде таблицы. По клику открывается форма редактирования строки
   - В форме редактирования строки кнопки добавления/сохранения и удаления строки

## Библиотеки
> AutoMapper\
> MediatR\
> EFC\
> Serilog

> Bootstrap\
> Bootstrap-select\
> Bootstrap-table\
> Moment\
> JQuery
