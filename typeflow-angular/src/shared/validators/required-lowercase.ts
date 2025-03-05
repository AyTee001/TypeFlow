import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function requiredLowercase(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if(!control ) return null;

        const value = control.value;
        if (!value) {
          return null;
        }

        const hasLowercase = /\p{Ll}/u.test(value);
        return hasLowercase ? null : { requiredLowercase: true };
    };
}