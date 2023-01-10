# WebApp

Тестовое задание
Реализуйте сервис для работы с данными магазина. Сервис должен состоять из Web API и
базы данных.
Данных, которые необходимо хранить/обрабатывать:

- Клиенты (ФИО, Дата рождения, Дата регистрации)
- Продукты (Название, Категория, Артикул, Цена)
- Покупки (Номер, Дата, Общая стоимость, Продукты)
!!! Учесть, что одна покупка может содержать несколько позиций, а
позиция - несколько единиц товара

Методы Web API (пути, формат данных на Ваше усмотрение):
1) Список именинников.
- Входящий параметр  - дата
- Возвращает список клиентов (id, ФИО) у которых сегодня день
рождение

2) Последние покупатели
 Входящий параметр - N - количество дней
 Возвращает список клиентов (id, ФИО), совершивших покупку за
последние N дней. Для каждого клиента также необходимо
выводить дату его последней покупки.

3) Востребованные категории
- Входящий параметр - идентификатор клиента
- Возвращает список категорий продуктов, которые покупал
найденный клиент. Для каждой категории возвращает количество
купленных единиц.

Для реализации желательно использовать:
 .net 5 и выше
 Для работы с БД, можете использовать ef core или ADO.NET
 реляционная бд (MS SQL, PostgreSQL, SQLite)

--------------------------------------------------------------------------------------------------------------------------------------------------
--- ТАК ЖЕ добавлен файл MS SQL SERVER QUERIES со всеми запросами (создание БД, создание таблиц, создание процедур, наполнение данными таблиц) ---
--------------------------------------------------------------------------------------------------------------------------------------------------
