﻿using System.Security.Claims;

namespace Neuromorfismo.Shared.Dto.UserAccount {
    public record Tokens(string AccessToken, string RefreshToken);

    public record AutenticarPorTokenDto(bool ActualizarSession, Tokens? Tokens = null);

}
