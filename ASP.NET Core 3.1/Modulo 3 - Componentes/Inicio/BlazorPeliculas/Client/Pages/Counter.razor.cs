﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculas.Client.Pages
{
    public class CounterBase:ComponentBase
    {
        [Inject] protected ServicioSingleton Singleton { get; set; }
        [Inject] protected ServicioTrasient Trasient { get; set; }
        [Inject] protected IJSRuntime JS { get; set;}


        protected int currentCount = 0;
        static int currentCountStatic = 0;
        [JSInvokable]
        public async Task IncrementCount()
        {
            currentCount++;
            Singleton.Valor = currentCount;
            Trasient.Valor = currentCount;
            currentCountStatic++;
            await JS.InvokeVoidAsync("pruebaPuntoNetStatic");
        }

        protected async Task IncrementCountJavascript()
        {
            await JS.InvokeVoidAsync("pruebaPuntoNETInstancia", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public static Task<int> ObtenerCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
