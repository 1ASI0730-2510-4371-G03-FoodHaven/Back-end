﻿namespace food_heaven_backend.Security.Domain.Comands;

public record SignUpCommand(String Username, String Password, String Role);