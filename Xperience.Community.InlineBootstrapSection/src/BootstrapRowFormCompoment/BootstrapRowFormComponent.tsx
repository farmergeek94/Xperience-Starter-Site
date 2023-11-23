import React, { useState } from 'react';

import { FieldInfoValueType, FormComponentProps, ValidationRuleProps } from '@kentico/xperience-admin-base';
import { FormItemWrapper } from '@kentico/xperience-admin-components';

interface BootstrapColumnModel {
    id?: string
    size: number,
    customClass: string
}

export const BootstrapRowFormComponent = (props: FormComponentProps) => {

    const getNewValues = () => {
        var values = [...props.value] as BootstrapColumnModel[];
        return values.map(x => { return { ...x } });
    }
    const addColumn = () => {
        if (props.onChange) {
            var values = getNewValues();
            values.push({ size: 12, customClass: "" });
            props.onChange(values);
        }
    }

    const removeColumn = (index: number) => {
        if (props.onChange) {
            var values = getNewValues();
            props.onChange(values.filter((x,i) => index != i));
        }
    }

    const updateSize = (index: number, size: string) => {
        if (props.onChange) {
            var values = getNewValues();
            values.forEach((x, i) => {
                if (i == index) {
                    x.size = parseInt(size);
                }
            });
            props.onChange(values);
        }
    }

    const updateClass = (index: number, customClass: string) => {
        if (props.onChange) {
            var values = getNewValues();
            values.forEach((x, i) => {
                if (i == index) {
                    x.customClass = customClass;
                }
            });
            props.onChange(values);
        }
    }

    // Renders the color selector and ensures propagation of the selected value
    return <FormItemWrapper
        label={props.label}
        explanationText={props.explanationText}
        invalid={props.invalid}
        validationMessage={props.validationMessage}
        markAsRequired={props.required}
        labelIcon={props.tooltip ? 'xp-i-circle' : undefined}
        labelIconTooltip={props.tooltip}>
        {(props.value as BootstrapColumnModel[]).map((x, i) => <div style={styles.container} key={i}>
            <div>
                <label style={styles.label}>Column Size</label>
                <div style={styles.wrapper}>
                    <select style={styles.components} className="form-control" value={x.size} onChange={(e) => updateSize(i, e.currentTarget.value) }>
                        <option value="12">12 Column (100%)</option>
                        <option value="11">11 Column (92%)</option>
                        <option value="10">10 Column (83%)</option>
                        <option value="9">9 Column (75%)</option>
                        <option value="8">8 Column (66%)</option>
                        <option value="7">7 Column (58%)</option>
                        <option value="6">6 Column (50%)</option>
                        <option value="5">5 Column (42%)</option>
                        <option value="4">4 Column (33%)</option>
                        <option value="3">3 Column (25%)</option>
                        <option value="2">2 Column (16%)</option>
                        <option value="1">1 Column (8%)</option>
                    </select>
                </div>
            </div>
            <div>
                <label style={styles.label} htmlFor={"bootstrap-row-section-custom-class" + i}>Custom Class</label>
                <div style={styles.wrapper}>
                    <input style={styles.components} type="text" id={"bootstrap-row-section-custom-class" + i} value={x.customClass} onChange={(e) => updateClass(i, e.currentTarget.value)} />
                </div>
            </div>
            {i > 0 && <button style={{ ...styles.button, ...styles.buttonDanger }} onClick={(e) => { e.preventDefault(); removeColumn(i) }}>Remove Column</button>}
        </div>)}
        <button style={{ ...styles.button, ...styles.buttonSuccess}} onClick={(e) => { e.preventDefault(); addColumn() }}>Add Column</button>
    </FormItemWrapper>;
};

interface BootstrapRowStyles {
    label: React.CSSProperties,
    components: React.CSSProperties,
    wrapper: React.CSSProperties,
    button: React.CSSProperties,
    buttonSuccess: React.CSSProperties,
    buttonDanger: React.CSSProperties,
    container: React.CSSProperties
}

const styles: BootstrapRowStyles = {
    label: {
        fontFamily: '"GT Walsheim",sans-serif',
        fontWeight: 400,
        fontSize: 14,
        lineHeight: "16px",
        color:"var(--color-text-low-emphasis)"
    },
    components: {
        fontFamily: '"Inter",sans-serif',
        fontWeight: 400,
        fontSize: 14,
        lineHeight: "20px",
        color: "var(--color-text-default-on-light)",
        resize: 'none',
        backgroundColor: "var(--color-input-background)",
        border: "none",
        padding: "8px 16px",
        outline: 'none',
        width: "100%"
    },
    wrapper: {
        borderRadius: "20px",
        border: "1px solid var(--color-input-border)",
        marginBottom: "5px"
    },
    button: {
        border: 'none',
        padding: '8px 16px',
        borderRadius: '20px',
        cursor: 'pointer'
    },
    buttonSuccess: {
        backgroundColor: "green",
        color: "white"
    },
    buttonDanger: {
        backgroundColor: "red",
        color: "white"
    },
    container: {
        border: "1px lightgray solid",
  padding: "5px",
  borderRadius: "10px",
  margin: "10px auto"
    }
}

