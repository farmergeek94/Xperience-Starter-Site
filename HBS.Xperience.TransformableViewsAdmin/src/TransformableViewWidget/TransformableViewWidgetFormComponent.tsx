import { FormComponentProps, useFormComponentCommandProvider } from '@kentico/xperience-admin-base'
import { Button, ButtonColor, DropDownSelectMenu, FormItemWrapper, Input, MenuItem, Select, TextArea, WindowPortal } from '@kentico/xperience-admin-components'
import React, { useEffect, useMemo, useState } from 'react'

import './TransformableViewWidgetFormComponent.css'
import { SelectListItem } from '../TransformableViewObjects/TransformableViewObjectsFormComponent'
import ViewsDialog from '../TransformableViewObjects/Components/ViewsDialog'
import InputTypeDialog from './Components/InputTypeDialog'

interface TransformableViewWidgetFormComponentModel {
    inputs?: InputType[]
    view?: string
    viewTitle?: string
    viewClassNames?: string
    viewCustomContent?: string
}

interface InputType {
    type: string
    name: string,
    value?:any | null
}

export const TransformableViewWidgetFormComponent = (props: FormComponentProps) => {
    const [views, setViews] = useState<SelectListItem[]>([])
    const [dialog, setDialog] = useState(false);
    const [viewsDialog, setViewsDialog] = useState(false);

    const value = useMemo(() => props.value as TransformableViewWidgetFormComponentModel, [props.value]);

    const update = (type: string, name: string, val?: any) => {
        const newValue = { ...value };
        const dict = newValue.inputs ? [...newValue.inputs] : [];
        if (dict.find(x => x.name == name)) {
            dict.forEach(x => {
                if (x.name == name) {
                    x.type = type;
                    x.name = name;
                    x.value = val;
                }
            });
        } else {
            dict.push({ type, name, value: val });
        }
        newValue.inputs = dict;
        props.onChange && props.onChange(newValue);
    }

    const updateModel = (model: TransformableViewWidgetFormComponentModel) => {
        props.onChange && props.onChange(model);
    }

    const { executeCommand } = useFormComponentCommandProvider();

    useEffect(() => {
        executeCommand && executeCommand<SelectListItem[]>(props, 'GetViews')
            .then(data => {
                if (data) {
                    setViews(data);
                }
            });
    }, [executeCommand]);

    return <FormItemWrapper
        label={props.label}
        explanationText={props.explanationText}
        invalid={props.invalid}
        validationMessage={props.validationMessage}
        markAsRequired={props.required}
        labelIcon={props.tooltip ? 'xp-i-circle' : undefined}
        labelIconTooltip={props.tooltip}
        childrenWrapperClassnames="transformable-view-widget">
        {value.inputs && value.inputs.map(x => {
            return <div style={{ paddingBottom: 10 }}>
                <Input label={x.name} value={x.value} type={ x.type as any } onChange={(e) => update(x.type ,x.name, e.currentTarget.value)} />
            </div>
        }) }

        <div style={{ paddingBottom: 15 }}>
            <Button label="Add Input" color={ButtonColor.Primary} onClick={() => setDialog(true)} />
        </div>


        <div style={{ paddingBottom: 15 }}>
            <div style={{ paddingBottom: 10 }}>
                <Input label="View" value={value.view} onChange={(e) => updateModel({ ...value, view: e.currentTarget.value })} />
            </div>
            <Button label="Select View" color={ButtonColor.Primary} onClick={() => setViewsDialog(true)} />
        </div>
        <div style={{ paddingBottom: 15 }}>
            <Input label="View Title" value={value.viewTitle} onChange={(e) => updateModel({ ...value, viewTitle: e.currentTarget.value })} />
        </div>
        <div style={{ paddingBottom: 15 }}>
            <Input label="View Class Names" value={value.viewClassNames} onChange={(e) => updateModel({ ...value, viewClassNames: e.currentTarget.value })} />
        </div>
        <div style={{ paddingBottom: 0 }}>
            <TextArea label="View Custom Content" value={value.viewCustomContent} onChange={(e) => updateModel({ ...value, viewCustomContent: e.currentTarget.value })} />
        </div>
        <div style={{ zIndex: 999999999 }}>
            <WindowPortal>
                {dialog && <InputTypeDialog open={dialog} setInputType={update} closeDialog={() => setDialog(false)} />}
                {viewsDialog && <ViewsDialog open={viewsDialog} props={props} onSelect={(x) => updateModel({ ...value, view: x })} closeDialog={() => setViewsDialog(false)} value={value.view} />}
            </WindowPortal>
        </div>
    </FormItemWrapper>
}