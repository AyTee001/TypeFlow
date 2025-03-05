import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function valueTypeEqualityValidator(compareToField: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if(!control || !control.parent) return null;

        const firstValue = control.value;
        const secondValue = control.parent.get(compareToField)?.value;

        if (firstValue !== secondValue) {
            return { notEqual: true };
        }
        
        return null;
    };
}