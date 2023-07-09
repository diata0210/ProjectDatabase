
function validatePassword(password) {
    if (password.length < 6) {
        return false;
    }
    return true;
}
function validateUniqueValue(value, valuesArray) {
    // Check if the value exists in the array
    if (valuesArray.includes(value)) {
        return false;
    }
    return true;
}

const form = document.querySelector('form');
form.addEventListener('submit', function (e) {
    const passwordInput = document.querySelector('#password');
    const usernameInput = document.querySelector('#username');
    const phoneInput = document.querySelector('#phone');
    const passwordError = document.querySelector('#passwordError');
    const usernameError = document.querySelector('#usernameError');
    const phoneError = document.querySelector('#phoneError');

    // Perform additional validation for username and phone uniqueness

    if (!validatePassword(passwordInput.value)) {
        passwordError.textContent = 'Password must be at least 6 characters long.';
        e.preventDefault();
    } else {
        passwordError.textContent = '';
    }

    if (!validateUniqueValue(usernameInput.value, usernames)) {
        usernameError.textContent = 'Username already exists. Please choose a different username.';
        e.preventDefault();
    } else {
        usernameError.textContent = '';
    }

    if (!validateUniqueValue(phoneInput.value, phones)) {
        phoneError.textContent = 'Phone number already exists. Please enter a different phone number.';
        e.preventDefault();
    } else {
        phoneError.textContent = '';
    }
});
