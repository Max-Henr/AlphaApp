const validateField = (field) => {
    let erroSpan = document.querySelector(`span[data-valmsg-for='${field.name}']`);
    if (!erroSpan) return;

    let errorMessage = "";
    let value = field.value.trim();

    if (field.hasAttribute("data-val-required") && value === "")
        errorMessage = field.getAttribute("data-val-required");

    if (field.hasAttribute("data-val-regex") && value !== "") {
        let patter = new RegExp(field.getAttribute("data-val-regex-pattern"))
        if (!patter.test(value))
            errorMessage = field.getAttribute("data-val-regex");
    }

    if (errorMessage) {
        field.classList.add("input-validation-error");
        errorSpan.classList.remove("field-validation-valid");
        errorSpan.classList.add("field-validation-error");
        errorSpan.textContent = errorMessage;
    } else {
        field.classList.remove("input-validation-error");
        errorSpan.classList.add("field-validation-valid");
        errorSpan.classList.remove("field-validation-error");
        errorSpan.textContent = "";
    }

};

const setupValidationForForm = (form) => {
    const fields = form.querySelectorAll("input[data-val='true'], textarea[data-val='true'], select[data-val='true']");
    fields.forEach(field => {
        field.addEventListener("input", () => validateField(field));
        field.addEventListener("change", () => validateField(field));
    });
    form.addEventListener("submit", (e) => {
        let allValid = true;
        fields.forEach(field => {
            const isValid = validateField(field);
            if (!isValid) {
                allValid = false;
            }
        });

        if (!allValid) {
            e.preventDefault(); 
        }
    });
};

document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    if (form) {
        setupValidationForForm(form);
    }
});