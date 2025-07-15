//personal-info

function EditInformation() {

    //Btn
    const subBtn = document.getElementById('subBtn');
    const editBtn = document.getElementById('editBtn');
    //lables
    const FirstNameLable = document.getElementById('FirstNameLable');
    const LastNameLable = document.getElementById('LastNameLable');
    const NationalCodeLable = document.getElementById('NationalCodeLable');
    const MobileLable = document.getElementById('MobileLable');
    const EmailLable = document.getElementById('EmailLable');
    //inputs
    const FirstNameInput = document.getElementById('FirstNameInput');
    const LastNameInput = document.getElementById('LastNameInput');
    const NationalCodeInput = document.getElementById('NationalCodeInput');
    const MobileInput = document.getElementById('MobileInput');
    const EmailInput = document.getElementById('EmailInput');

    editBtn.hidden = true;
    subBtn.hidden = false;

    FirstNameLable.hidden = true;
    LastNameLable.hidden = true;
    NationalCodeLable.hidden = true;
    MobileLable.hidden = true;
    EmailLable.hidden = true;

    FirstNameInput.hidden = false;
    LastNameInput.hidden = false;
    NationalCodeInput.hidden = false;
    MobileInput.hidden = false;
    EmailInput.hidden = false;

}

function CancelInformation() {

    //Btn
    const subBtn = document.getElementById('subBtn');
    const editBtn = document.getElementById('editBtn');
    //lables
    const FirstNameLable = document.getElementById('FirstNameLable');
    const LastNameLable = document.getElementById('LastNameLable');
    const NationalCodeLable = document.getElementById('NationalCodeLable');
    const MobileLable = document.getElementById('MobileLable');
    const EmailLable = document.getElementById('EmailLable');
    //inputs
    const FirstNameInput = document.getElementById('FirstNameInput');
    const LastNameInput = document.getElementById('LastNameInput');
    const NationalCodeInput = document.getElementById('NationalCodeInput');
    const MobileInput = document.getElementById('MobileInput');
    const EmailInput = document.getElementById('EmailInput');

    editBtn.hidden = false;
    subBtn.hidden = true;

    FirstNameLable.hidden = false;
    LastNameLable.hidden = false;
    NationalCodeLable.hidden = false;
    MobileLable.hidden = false;
    EmailLable.hidden = false;

    FirstNameInput.hidden = true;
    LastNameInput.hidden = true;
    NationalCodeInput.hidden = true;
    MobileInput.hidden = true;
    EmailInput.hidden = true;

}

//personal-info