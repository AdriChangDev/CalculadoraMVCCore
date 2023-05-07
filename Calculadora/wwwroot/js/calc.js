function CalculadoraCientifica() {
    this.listaOperacionesHTML = document.getElementById(
        "listaOperacionesBasicas"
    );
    this.listaHTML = document.getElementById("listaNumeros");
    this.listaOperacionesMathHTML = document.getElementById(
        "listaOperacionesMath"
    );
    this.displayNow = document.getElementById("result");
    this.displaySave = document.getElementById("resultSave");
    this.listaNumeros = document.getElementById("listaNumeros");
    this.isResolve = false;
    this.isBasic = false;
    this.memory = 0;
    this.result = 0;
}

CalculadoraCientifica.prototype.numeros = ["=", ".", "0", "3", "2", "1", "6", "5", "4", "9", "8", "7",];

CalculadoraCientifica.prototype.operacionesBasicas = ["DEL", "AC", "+", "-", "*", "/", "(", ")",];


CalculadoraCientifica.prototype.operacionMath = ["sen(", "cos(", "tg(", "arctg(", "√(", "log(", "ln(", "±", "ⅹ²", "|ⅹ|", "ⅹ³", "⅟", "%", "‰", "π", "ɘ", "ͳ", "10ⁿ", "Rd", "x!", "MC", "MR", "M+", "M-"];

CalculadoraCientifica.prototype.letrasGriegas = ["π", "ɘ", "τ"];

CalculadoraCientifica.prototype.numeros.reverse();

CalculadoraCientifica.prototype.mr = function (calc) {
    displayNow.innerText = calc.memory;
};

CalculadoraCientifica.prototype.mc = function (calc) {
    calc.memory = 0;
    this.ScreenValueNow(displayNow, 0);
};

CalculadoraCientifica.prototype.mp = function (calc) {
    calc.memory += parseFloat(displayNow.innerText);
    this.ScreenValueNow(displayNow, 0);
};

CalculadoraCientifica.prototype.mm = function (calc) {
    calc.memory -= parseFloat(displayNow.innerText);
};

CalculadoraCientifica.prototype.addNumero = function (valor) {
    var estaComprendido = listaOperacionesBasicas.includes(valor);
    var resultado = displayNow.innerText;

    if (
        (displayNow.innerText == "0" || displayNow.innerText == "Error" || calc.letrasGriegas.includes(displaySave.innerText) || this.isResolve == true) &&
        !estaComprendido
    ) {
        displayNow.innerHTML = "";
    }
    this.isResolve = false;
    switch (valor) {
        case "=":
            this.ScreenValueSafe(displaySave, displayNow.innerText);
            this.isResolve = true;
            if (!displayNow.innerText == "0") {
                var resultado = this.equal(displayNow.innerText);
                CallAjaxAddOperation(displaySave.innerText, resultado)
                this.ScreenValueSafe(displayNow, resultado);

            } else {
                this.clear();
            }
            break;
        case "AC":
            this.clear();
            break;
        case "DEL":

            this.ScreenValueSafe(displayNow, this.deleteLastOne(resultado));
            break;
        case "|ⅹ|":

            var numero = this.absolute(resultado);
            if (isNaN(numero)) {
                this.ScreenValueSafe(displayNow, 0);
            } else {
                this.ScreenValueSafe(displayNow, numero);
                this.isResolve = true;
            }
            break;
        case "±":
            this.ScreenValueSafe(displayNow, this.changeSign(resultado));
            break;
        case "ⅹ²":
            this.ScreenValueSafe(displayNow, this.elevateGeneral(resultado, 2));
            break;
        case "ⅹ³":
            this.ScreenValueSafe(displayNow, this.elevateGeneral(resultado, 3));
            displayNow.style.color = "black";
            break;
        case "⅟":
            this.ScreenValueSafe(displayNow, this.oneDivided(resultado));
            break;
        case "%":
            this.ScreenValueSafe(displayNow, this.calculatePercentage(resultado, 0.01));
            break;
        case "‰":
            this.ScreenValueSafe(displayNow, this.calculatePercentage(resultado, 0.001));
            break;
        case "10ⁿ":
            this.ScreenValueSafe(displayNow, this.elevateGeneral(10, resultado));
            break;
        case "x!":
            this.ScreenValueSafe(displayNow, this.factorialNumber(resultado));
            break;
        case "Rd":
            this.ScreenValueSafe(displayNow, this.Random());
            break;
        case "MR":
            this.mr(calc);
            break;
        case "MC":
            this.mc(calc);
            break;
        case "M-":
            this.mm(calc);
            break;
        default:
            this.ScreenValueNow(displayNow, this.defaultButtons(displayNow.innerText, valor));

    }
};

CalculadoraCientifica.prototype.testRepeat = function (lista, valor) { return (lista.includes(valor) && lista.includes(displayNow.innerText.slice(-1)) && valor != "("); };

CalculadoraCientifica.prototype.replaceCharacter = function (expresion) {
    var replaceChars = {
        π: Math.PI.toFixed(4),
        ɘ: Math.E.toFixed(4),
        ͳ: 2 * Math.PI.toFixed(4),
        sen: "Math.sin",
        cos: "Math.cos",
        tg: "Math.tanh",
        arctg: "Math.atan2",
        "√": "Math.sqrt",
        log: "Math.log10",
        ln: "Math.log",
    };
    for (var caracter in replaceChars) {
        if (replaceChars.hasOwnProperty(caracter)) {
            expresion = expresion.replace(
                new RegExp(caracter, "g"),
                replaceChars[caracter]
            );
        }
    }

    return expresion;
};

CalculadoraCientifica.prototype.equal = function (expresion) {
    try {
        return new Function("return " + this.replaceCharacter(expresion))();
    } catch (e) {
        return "Error";
    }
};

CalculadoraCientifica.prototype.clear = function () {
    displayNow.innerText = "0";
    displaySave.innerText = "0";
};

CalculadoraCientifica.prototype.absolute = function (expresion) { return Math.abs(parseFloat(expresion)); };

CalculadoraCientifica.prototype.moreless = function (expresion) { return parseFloat(expresion) * -1; };

CalculadoraCientifica.prototype.deleteLastOne = function (expresion) {
    if (expresion == "Error" || isNaN(expresion)) {
        return "0";
    } else {
        expresionNueva = this.isLastOneZero(expresion);
        return expresionNueva === "" ? "0" : expresionNueva;
    }
};

CalculadoraCientifica.prototype.defaultButtons = function (expresion, valor) {
    if (!this.testRepeat(listaOperacionesBasicas, valor) && !this.testRepeat(listaOperacionesMath, valor)) {
        return (expresion += valor);
    } else {
        var ultimoCarac = expresion.slice(-1);
        if (ultimoCarac == ")") {
            return (expresion += valor);
        } else {
            var expresionAntigua = expresion.slice(0, -1);
            var expresionNueva = expresionAntigua + valor;
            return expresionNueva;
        }
    }
};

CalculadoraCientifica.prototype.isLastOneZero = function (expresion) {
    var ultimoCaracter = expresion.slice(-1);
    if (listaOperacionesMath.includes(ultimoCaracter)) {
        // El último carácter es un elemento completo de operacionMath
        return expresion.slice(0, -ultimoCaracter.length);
    } else {
        // El último carácter no es un elemento completo de operacionMath
        return expresion.slice(0, -1);
    }
};

CalculadoraCientifica.prototype.factorialNumber = function (expresion) {
    this.isResolve = true;
    var resultado = 1;
    var numero = this.equal(expresion);
    return (function () {
        for (let i = 2; i <= numero; i++) {
            resultado *= i;
        }
        return resultado;
    })();
};

CalculadoraCientifica.prototype.Random = function () {
    this.isResolve = true;
    return Math.random().toFixed(5);
};

CalculadoraCientifica.prototype.calculatePercentage = function (resultado, porcentaje) {
    this.isResolve = true;
    return resultado * porcentaje;
};

CalculadoraCientifica.prototype.elevateGeneral = function (numero, elevado) {
    this.isResolve = true;
    return Math.pow(this.equal(this.replaceCharacter(numero)), elevado);
};

CalculadoraCientifica.prototype.changeSign = function (numero) {
    return numero * -1;
};

CalculadoraCientifica.prototype.oneDivided = function (numero) {
    this.isResolve = true;
    return 1 / parseFloat(numero);
};
CalculadoraCientifica.prototype.ScreenValueNow = function (ScreenNow, value) {
    ScreenNow.innerText = value;
    if (this.equal(value) == "Error") {
        ScreenNow.style.color = "red";
    } else {
        ScreenNow.style.color = "black";
    }
};

CalculadoraCientifica.prototype.ScreenValueSafe = function (ScreenSave, value) {
    ScreenSave.innerText = value;
};

CalculadoraCientifica.prototype.changeBasic = function () {
    if (!this.isBasic) {
        document.getElementById("hidde").style.display = "none";
        this.isBasic = true;
    } else {
        document.getElementById("hidde").style.display = "block";
        this.isBasic = false;
    }
};

window.onload = function () {
    addButtons();
};

var calc = new CalculadoraCientifica();
//TAKE LIST OF CALC
var listaNumeros = calc.numeros;
var listaOperacionesBasicas = calc.operacionesBasicas;
var listaOperacionesMath = calc.operacionMath;

//CONFIGURE BY ID
var listaOperacionesHTML = calc.listaOperacionesHTML;
var listaHTML = calc.listaNumeros;
var listaOperacionesMathHTML = calc.listaOperacionesMathHTML;
//SCREEN
var displayNow = calc.displayNow;
var displaySave = calc.displaySave;

function addButtons() {
    clearList(listaHTML);
    clearList(listaOperacionesHTML);
    clearList(listaOperacionesMathHTML);
    addNumber(listaOperacionesHTML, 2, listaOperacionesBasicas);
    addNumber(listaHTML, 3, listaNumeros, "numero");
    addNumber(listaOperacionesMathHTML, 6, listaOperacionesMath);
}

function clearList(listaBase) {
    while (listaBase.firstChild) {
        listaBase.removeChild(listaBase.firstChild);
    }
}

function addNumber(listaBase, num, listaNumeros) {
    for (let index = 0; index < listaNumeros.length; index++) {
        if (index % num === 0) {
            var divElement = document.createElement("div");
            divElement.setAttribute("class", "group");
        }
        divElement.appendChild(createNumberElement(listaNumeros[index]));
        listaBase.appendChild(divElement);
    }
}

function createNumberElement(numero) {
    if (numero == "DEL" || numero == "AC") {
        var liBoton = document.createElement("button");
        liBoton.innerText = numero;
        liBoton.classList.add("boton");
        liBoton.setAttribute("style", "background-color: #e71f1fa1;");

        liBoton.setAttribute("onclick", "calc.addNumero('" + numero + "')");
    } else {
        var liBoton = document.createElement("button");
        liBoton.innerText = numero;
        liBoton.classList.add("boton");
        liBoton.setAttribute("onclick", "calc.addNumero('" + numero + "')");
    }
    return liBoton;
}
/*CALL TO AJAX */
function CallAjaxAddOperation(expresionMatematica, Resultado) {
    $.ajax({
        type: "POST",
        url: "/Home/Addition",
        data: { operacion: expresionMatematica, resultado: Resultado },
        success: function (result) {
            console.log(result);
        },
        error: function (error) {
            console.log(error);
        }
    });

}
