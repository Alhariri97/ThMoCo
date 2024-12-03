Add a Migration:
dotnet ef migrations add [MigrationName] --output-dir Data/Migrations
Apply Migrations:
dotnet ef migrations script --idempotent -- --environment Production
This command create the structure of the db will need to eather apply it to the live db or coppy the out put and run it in db text editor


Once pushed to github a pipeline will run and deploy to azure web app api : thamcowebappapi-bdaybvcbe4c8hubg.uksouth-01.azurewebsites.net

the following end points will be avalibale: 

GET /api/products - Retrieve all products
GET /api/products/{id} - Retrieve a product by ID
POST /api/products - Create a new product
PUT /api/products/{id} - Update an existing product
DELETE /api/products/{id} - Delete a product

