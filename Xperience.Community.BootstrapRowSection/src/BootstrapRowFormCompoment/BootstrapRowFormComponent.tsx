import React, { useState } from 'react';

import { FormComponentProps } from '@kentico/xperience-admin-base';
import { Button, ButtonColor, ButtonSize, FormItemWrapper, Input, MenuItem, Select } from '@kentico/xperience-admin-components';

import "./BootstrapRowFormComponent.css"
interface BootstrapColumnModel {
    id?: string
    size: number,
    customClass: string,
    gutterY: string,
    gutterX: string
}

export const BootstrapRowFormComponent = (props: FormComponentProps) => {

    const getNewValues = () => {
        var values = [...props.value] as BootstrapColumnModel[];
        return values.map(x => { return { ...x } });
    }
    const addColumn = () => {
        if (props.onChange) {
            var values = getNewValues();
            values.push({ size: 12, customClass: "", gutterX: "", gutterY: "" });
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

    const updateGutterY = (index: number, gutter: string) => {
        if (props.onChange) {
            var values = getNewValues();
            values.forEach((x, i) => {
                if (i == index) {
                    x.gutterY = gutter;
                }
            });
            props.onChange(values);
        }
    }

    const updateGutterX = (index: number, gutter: string) => {
        if (props.onChange) {
            var values = getNewValues();
            values.forEach((x, i) => {
                if (i == index) {
                    x.gutterX = gutter;
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
        labelIconTooltip={props.tooltip}
        childrenWrapperClassnames="bootstrap-row">
        {(props.value as BootstrapColumnModel[]).map((x, i) => {
            return <div style={{
                border: "1px lightgray solid",
                padding: "10px",
                borderRadius: "20px",
                margin: "10px auto"
            }} key={i}>
                <div style={{ marginBottom: 15 }}>
                    <Select label="Column Size" value={x.size.toString()} onChange={(e) => updateSize(i, e ?? "")}>
                        <MenuItem primaryLabel="12 Column (100%)" value="12" />
                        <MenuItem primaryLabel="11 Column (92%)" value="11" />
                        <MenuItem primaryLabel="10 Column (83%)" value="10" />
                        <MenuItem primaryLabel="9 Column (75%)" value="9" />
                        <MenuItem primaryLabel="8 Column (66%)" value="8" />
                        <MenuItem primaryLabel="7 Column (58%)" value="7" />
                        <MenuItem primaryLabel="6 Column (50%)" value="6" />
                        <MenuItem primaryLabel="5 Column (42%)" value="5" />
                        <MenuItem primaryLabel="4 Column (33%)" value="4" />
                        <MenuItem primaryLabel="3 Column (25%)" value="3" />
                        <MenuItem primaryLabel="2 Column (16%)" value="2" />
                        <MenuItem primaryLabel="1 Column (8%)" value="1" />
                    </Select>
                </div>
                <div style={{ display: 'flex', flexDirection: "row", gap: 10, marginBottom: 15 }}>
                    <div style={{ flex: "1 1 50%" }}>
                        <Input label="Custom Class" value={x.customClass} onChange={(e) => updateClass(i, e.currentTarget.value)} />
                    </div>
                    <div style={{ flex: "1 1 25%" }}>
                        <Select label="Gutter Y" value={x.gutterY} onChange={(e) => updateGutterY(i, e ?? "")}>
                            <MenuItem primaryLabel="Inherit" value="" />
                            <MenuItem primaryLabel="No Gutter (gy-0)" value="gy-0" />
                            <MenuItem primaryLabel="(gy-1)" value="gy-1" />
                            <MenuItem primaryLabel="(gy-2)" value="gy-2" />
                            <MenuItem primaryLabel="(gy-3)" value="gy-3" />
                            <MenuItem primaryLabel="(gy-4)" value="gy-4" />
                            <MenuItem primaryLabel="(gy-5)" value="gy-5" />
                        </Select>
                    </div>
                    <div style={{ flex: "1 1 25%" }}>
                        <Select label="Gutter X" value={x.gutterX} onChange={(e) => updateGutterX(i, e ?? "")}>
                            <MenuItem primaryLabel="Inherit" value="" />
                            <MenuItem primaryLabel="No Gutter (gx-0)" value="gx-0" />
                            <MenuItem primaryLabel="(gx-1)" value="gx-1" />
                            <MenuItem primaryLabel="(gx-2)" value="gx-2" />
                            <MenuItem primaryLabel="(gx-3)" value="gx-3" />
                            <MenuItem primaryLabel="(gx-4)" value="gx-4" />
                            <MenuItem primaryLabel="(gx-5)" value="gx-5" />
                        </Select>
                    </div>
                </div>
                {i > 0 && <Button label="Remove Column" color={ ButtonColor.Tertiary } size={ButtonSize.S} onClick={(e) => { e.preventDefault(); removeColumn(i) }} />}
            </div>
        })}
        <Button label="Add Column" onClick={(e) => { e.preventDefault(); addColumn() }} size={ButtonSize.S} color={ ButtonColor.Primary } />
    </FormItemWrapper>;
};
