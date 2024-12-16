# Terraform Installation and Execution Instructions

Before running Terraform commands, create a `settings.json` file in the Terraform directory with the following structure:
```json
{
    "access_key": "<YOUR_ACCESS_KEY>", // access key to you storage account in the RG the precreated by you in Azure, that needed by TF , to save the TF status.
    "client_id": "<YOUR_CLIENT_ID>",
    "client_secret": "<YOUR_CLIENT_SECRET>"
}
```
Run the followaing commands in your terminal which will load you setting.josn
## Read settings.json file into a PowerShell object

```
$settings = Get-Content -Raw -Path ".\settings.json" | ConvertFrom-Json
```


## Extract values from the settings
```
$access_key = $settings.access_key
$client_id = $settings.client_id
$client_secret = $settings.client_secret
```


# For DevAh env.
# Run the Terraform command with the extracted values

```
terraform init -backend-config=".\environments\devah\devah-backend.tfvars" -backend-config="access_key=$access_key" -reconfigure
```
```
terraform plan -var-file=".\environments\devah\devah.tfvars" -var "client_id=$client_id" -var "client_secret=$client_secret"
```
```
terraform apply -auto-approve -var-file=".\environments\devah\devah.tfvars" -var "client_id=$client_id" -var "client_secret=$client_secret"
```


Following this method will keep my credentials save and not have to push them to my VCS (version control system).