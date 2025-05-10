**TUYA - CREDIT CARDS**

API Proyect to manage credit cards, allows users to purchase products, and view transaction tracking.
This API serves as the backend for a web application.

**Technology**

-	**IDE**: Visual Studio 2022
-	**FRAMEWORK**: .NET
-	**VERSION**: 8
-	**DATABASE**: Microsoft SQL Server 2016 (SP2) (KB4052908) - 13.0.5026.0 (X64)

**Languages:** C#

**Layers**

-	**WebApi**: Contains the API project and exposes HTTP endpoints to the frontend.

-	**Application**: Implements the use cases. Receive request from WebApi project and coordinates business logic.

-	**DTO**: Defines business entities, data transfer objects, and enums used across layers.

-	**Infrastructure**: Layer responsible for data access, database context and repository implementations.

-	**Shared**: Includes:
	-	**Common**: Generic helpers, validators and constants.
	-	**CrossCutting**: Registers dependecies and API configurations (IoC, middleware, etc).
