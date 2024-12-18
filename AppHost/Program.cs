using Smartify.Shared;

var builder = DistributedApplication.CreateBuilder(args);

#region resources
var keycloakUser = builder.AddParameter("keycloak-user", secret:true);
var keycloakPassword = builder.AddParameter("keycloak-password", secret: true);
var keycloak = builder
  .AddKeycloak(name: Resources.Services.Keycloak.NAME, port: Resources.Services.Keycloak.PORT, adminUsername: keycloakUser, adminPassword: keycloakPassword)
  .WithExternalHttpEndpoints()
  .WithDataVolume();

#endregion


#region projects

builder
  .AddProject<Projects.Smartify_Ingress>(Resources.Projects.Ingress.NAME)
  .WithHttpsEndpoint(port: Resources.Projects.Ingress.PORT)
  .WithReference(keycloak).WaitFor(keycloak);

#endregion

builder.Build().Run();
