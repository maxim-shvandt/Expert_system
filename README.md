# C# example of simple expert system

Содержание:
•	Цель работы
•	Задание
•	Описание структур данных и основных функций
•	Протоколы проведенных экспериментов
•	Исходный код программы
Цель работы. 
Разработка модельной программной системы представления знаний на основе семантической сети (СС).
Задания к лабораторной работе
•	Разработать структуры данных для хранения в памяти компьютера списка записей (таблицы) объектов, типов отношений и связей между объектами (не более 50 экземпляров для каждого типа записей).
•	Разработать программу, обеспечивающую выполнение следующих функций:
-	ввод из текстового файла и сохранение в памяти модели представления знаний СС (см. пример в Приложении 2);
-	вывод на экран по запросу списка всех объектов, связей и отношений между объектами;
-	ввод с клавиатуры запроса предопределённого формата к базе знаний СС, поиск ответа и вывод его на экран (запрос и ответ должны быть преобразованы в читабельную форму).
Описание структур данных и основных функций
Программа написана на языке программирования C#. Для неё был создан специальный класс ExpertSystem, который содержит 2 основных метода: парсинга данных (включая наследование связей), который принимает сроку-путь к файлу с описанием СС и метод ответа на вопросы, который принимает строку запроса к СС. 
Чтобы разрешить пользователю присваивать произвольные номера объектам и типам, программно была реализована система внешних и внутренних ключей, т. е. в файле с описанием СС объект может иметь номер 3333, но в базу он будет записан под номером, соответствующим его положению в порядке объектов, для этого в программе использованы такие встроенные структуры данных как Dictionary и List. Благодаря им мы можем совершать обход по данным не привязывясь жестко к порядку нумирования объектов и связей.
Для типов же данная система была реализована для того, чтобы дать возможность пользователю создавать несколько разных связей одинакового типа, например связь «вес» типа «0» и связь сын типа «0». Всего в системе поддерживается 3 типа связей:
•	0 – нет наследования свойств.
•	1 – первоочередное наследование (например отношение is-a).
•	2 – наследование второй очерёдности (например has-part).
С одним лишь предположением системы – нумерация связей (не типов) будет происходить в файле с 1, номер связи «0» будет соответствовать отсутствию связи. Таким образом получаем, что из последовательности связей 1->1 наследуется 1, из 2->2 наследуется 2, из 1->2 наследуется 2, из 2->1 наследуется 2, т. е. наследуется максимальный тип связи (кроме 0).
Для сохранения был создан список структур, хранящий <код, имя 1 объекта><код, имя связи><код, имя 2 объекта>

Тесты
 
#1
1:plane
2:flying object
3:swallow
4:wings
5:Boeing 787
6:Juko
7:some weight
8:feather (coverege)
11:sparrow
18:Steve
277:space ship
133:Jack
1701:USS Enterprise
47:Light object
85:big thing

#2
1:has part:2
2:is a:1
3:weights:0
7:nestling of:0

#3
1701:2:2
1:2:2
3:2:2
2:1:4
4:1:8
2:3:7
1701:2:277
5:2:1
6:2:3
8:3:7
11:2:2
18:2:11
133:7:18
277:2:85


Выполнение:
18::2::11
133::7::18
277::2::85
========== Processed data =============
USS Enterprise is a flying object
plane is a flying object
swallow is a flying object
flying object has part wings
wings has part feather (coverege)
flying object weights some weight
USS Enterprise is a space ship
Boeing 787 is a plane
Juko is a swallow
feather (coverege) weights some weight
sparrow is a flying object
Steve is a sparrow
Jack nestling of Steve
space ship is a big thing
USS Enterprise has part wings
USS Enterprise has part feather (coverege)
USS Enterprise weights some weight
plane has part wings
plane has part feather (coverege)
plane weights some weight
swallow has part wings
swallow has part feather (coverege)
swallow weights some weight
flying object has part feather (coverege)
flying object weights some weight
wings weights some weight
USS Enterprise is a big thing
Boeing 787 is a flying object
Boeing 787 has part wings
Boeing 787 has part feather (coverege)
Boeing 787 weights some weight
Juko is a flying object
Juko has part wings
Juko has part feather (coverege)
Juko weights some weight
sparrow has part wings
sparrow has part feather (coverege)
sparrow weights some weight
Steve is a flying object
Jack is a sparrow
========== Processed data in codes =============
1701:2:2
1:2:2
3:2:2
2:1:4
4:1:8
2:3:7
1701:2:277
5:2:1
6:2:3
8:3:7
11:2:2
18:2:11
133:7:18
277:2:85
1701:1:4
1701:1:8
1701:3:7
1:1:4
1:1:8
1:3:7
3:1:4
3:1:8
3:3:7
2:1:8
2:3:7
4:3:7
1701:2:85
5:2:2
5:1:4
5:1:8
5:3:7
6:2:2
6:1:4
6:1:8
6:3:7
11:1:4
11:1:8
11:3:7
18:2:2
133:2:11
==========-----------------------=============
========== The data is read and processed. =============
========== Enter the query by example: < ? : ? : ? > =============
?:?:?
========== ANSWER =============
USS Enterprise : is a : flying object
plane : is a : flying object
swallow : is a : flying object
flying object : has part : wings
wings : has part : feather (coverege)
flying object : weights : some weight
USS Enterprise : is a : space ship
Boeing 787 : is a : plane
Juko : is a : swallow
feather (coverege) : weights : some weight
sparrow : is a : flying object
Steve : is a : sparrow
Jack : nestling of : Steve
space ship : is a : big thing
USS Enterprise : has part : wings
USS Enterprise : has part : feather (coverege)
USS Enterprise : weights : some weight
plane : has part : wings
plane : has part : feather (coverege)
plane : weights : some weight
swallow : has part : wings
swallow : has part : feather (coverege)
swallow : weights : some weight
flying object : has part : feather (coverege)
flying object : weights : some weight
wings : weights : some weight
USS Enterprise : is a : big thing
Boeing 787 : is a : flying object
Boeing 787 : has part : wings
Boeing 787 : has part : feather (coverege)
Boeing 787 : weights : some weight
Juko : is a : flying object
Juko : has part : wings
Juko : has part : feather (coverege)
Juko : weights : some weight
sparrow : has part : wings
sparrow : has part : feather (coverege)
sparrow : weights : some weight
Steve : is a : flying object
Jack : is a : sparrow
===============================
========== Enter the query by example: < ? : ? : ? > =============
6:?:?
========== ANSWER =============
Juko : is a : swallow
Juko : is a : flying object
Juko : has part : wings
Juko : has part : feather (coverege)
Juko : weights : some weight
===============================
========== Enter the query by example: < ? : ? : ? > =============
?:2:2
========== ANSWER =============
USS Enterprise : is a : flying object
plane : is a : flying object
swallow : is a : flying object
sparrow : is a : flying object
Boeing 787 : is a : flying object
Juko : is a : flying object
Steve : is a : flying object
===============================
========== Enter the query by example: < ? : ? : ? > =============
2:0:?
========== ANSWER =============
flying object has no relation with other objects
===============================
========== Enter the query by example: < ? : ? : ? > =============
1701:3:7
========== ANSWER =============
Yes
USS Enterprise weights some weight
===============================
========== Enter the query by example: < ? : ? : ? > =============
