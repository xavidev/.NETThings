using FluentAssertions;
using NUnit.Framework;
using System;
namespace TDD
{
    [TestFixture]
    public class PilaTest
    {
        private Pila pila;

        [SetUp] // Se va realizar antes de ejecutar cualquier prueba.
        public void Setup(){
            pila = new Pila();
        }

        [Test]
        public void CreoUnapila_LaPila_DebeEstarVacia()
        {
           
            var estado = pila.Vacia(); // Act --> Ejecutar el código/accion a probar.
            pila.Vacia().Should().Be(true); // Assert --> Verificación/Validación.
        }
    }
}
