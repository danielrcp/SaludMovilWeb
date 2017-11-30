
function fixedlength(textboxID, maxlength) {
    if (textboxID.value.length > maxlength) {
        textboxID.value = textboxID.value.substr(0, maxlength);
    }
    if (textboxID.value.length < maxlength) {
        textboxID.style.borderColor = '#FD5E53';
        return false;
    }
    if (textboxID.value.length == maxlength) {
        textboxID.style.borderColor = '#017e01';
        return true;
    }

    else if (textboxID.value.length < maxlength || textboxID.value.length == maxlength) {
        textboxID.value = textboxID.value.replace(/[^\d]+/g, '');
        return true;
    }
    else
        EvalError('Por favor ingresar solo números');
}


function AlertaMinimaTexto(textboxID, maxlength)
{
    if (textboxID.value.length < maxlength)
    {
        alert("Número de contacto incorrecto, por favor validar");
        return false;
    }
}



function validarCorreo(txtID) {
    var email = txtID.value;
    var regex = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
    if (email.match(regex)) {
        txtID.style.borderColor = '#017e01';
    }
    else {
        txtID.style.borderColor = '#FD5E53';
        return false;
    }
}

