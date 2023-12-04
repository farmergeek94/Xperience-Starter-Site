import { FormComponentProps, useFormComponentCommandProvider } from '@kentico/xperience-admin-base'
import React, { createContext, useContext, useEffect, useMemo, useReducer, useState } from 'react'
import CategoryItem from '../Shared/CategoryItem';
import { Button, ButtonColor, ButtonSize, Dialog, FormItemWrapper, Icon, Input, TreeNode, TreeNodeLeadingIcon, TreeNodeTitle, TreeNodeTrailingIcon, TreeView } from '@kentico/xperience-admin-components';
import HierarchicalCategories from '../Shared/HierarchicalCategories';
import './CategoryListFormComponent.css'

interface CategoyListFormComponentState {
    dialogOptions: CategoryListFormComponentDialogOptions
    categories: CategoryItem[]
}

interface CategoryListFormComponentDialogOptions {
    isOpen: boolean
    selectedCategories: number[]
}

type CategoyListFormComponentActions =
    { type: "dialog-options", data: CategoryListFormComponentDialogOptions }
    | { type: "categories", data: CategoryItem[] }

const CategoyListFormComponentReducer = (state: CategoyListFormComponentState, action: CategoyListFormComponentActions) => {
    const { type, data } = action;
    const newState = { ...state };
    switch (type) {
        case "categories":
            newState.categories = data;
            break;
        case "dialog-options":
            newState.dialogOptions = data;
            break;
    }
    return newState;
}

interface ICategoyListFormComponentContext {
    categories: CategoryItem[],
    selectedCategories: number[]
    addCategory?: (x: number) => void,
    removeCategory?: (x: number) => void
}

const CategoyListFormComponentContext = createContext<ICategoyListFormComponentContext>({ categories: [], selectedCategories: [] });

export const CategoryListFormComponent = (props: FormComponentProps) => {
    var [state, dispatch] = useReducer(CategoyListFormComponentReducer, {
        categories: []
        , dialogOptions: {
            isOpen: false,
            selectedCategories: []
        }
    })

    const { executeCommand } = useFormComponentCommandProvider();

    useEffect(() => {
        executeCommand<CategoryItem[]>(props, 'GetCategories')
            .then(data => {
                if (data) {
                    dispatch({
                        type: "categories",
                        data
                    });
                    var ids = data.map(x => x.categoryID);
                    var currentCategories = (props.value as number[]);
                    var validCategories = (props.value as number[]).filter(x => ids.indexOf(x) > -1);
                    if (currentCategories.length != validCategories.length) {
                        props.onChange && props.onChange(validCategories);
                    }
                }
            });
    }, []);

    const addCategory = (value: number) => {
        var selectedCategories = [...state.dialogOptions.selectedCategories];
        selectedCategories.push(value);
        dispatch({
            type: "dialog-options", data: {
                isOpen: true, selectedCategories
            }
        });
    }
    const removeCategory = (value: number) => {
        var selectedCategories = state.dialogOptions.selectedCategories.filter(x => x != value);
        dispatch({
            type: "dialog-options", data: {
                isOpen: true, selectedCategories
            }
        });
    }

    const removeCategoryManual = (value: number) => {
        var values = (props.value as number[]).filter(x => x != value);
        props.onChange && props.onChange(values);
    }

    const openDialog = () => {
        dispatch({
            type: "dialog-options", data: {
                isOpen: true, selectedCategories: props.value
            }
        });
    }

    const handleClose = () => {
        dispatch({
            type: "dialog-options", data: {
                isOpen: false, selectedCategories: []
            }
        });
    }

    const handleSave = () => {
        props.onChange && props.onChange(state.dialogOptions.selectedCategories);
        handleClose();
    }


    const hierarchicalList = useMemo(() => {
        return HierarchicalCategories(state.categories);
    }, [state.categories]);

    return <FormItemWrapper
        label={props.label}
        explanationText={props.explanationText}
        invalid={props.invalid}
        validationMessage={props.validationMessage}
        markAsRequired={props.required}
        labelIcon={props.tooltip ? 'xp-i-circle' : undefined}
        labelIconTooltip={props.tooltip}
        childrenWrapperClassnames="category-list">
        {(props.value as number[]).length > 0 && <div style={{ display: "flex", flexDirection: "row", flexWrap: 'wrap', alignItems: 'flex-start', border: "1px solid lightgrey", borderRadius: "25px", padding: 10, marginBottom: 10 }}>
            {(props.value as number[]).map(x => <Button key={x} label={state.categories.find(c => c.categoryID == x)?.categoryDisplayName} color={ButtonColor.Tertiary} size={ButtonSize.XS} trailingIcon="xp-minus-circle" onClick={() => removeCategoryManual(x)} />)}
        </div>}
        <Button label={"Select Categories"} onClick={openDialog} color={ButtonColor.Primary} />
        {state.dialogOptions.isOpen && <Dialog
            isOpen={state.dialogOptions.isOpen}
            headline="Select Categories"
            isDismissable={true}
            headerCloseButton={{ tooltipText: "Close Dialog" }}
            confirmAction={{ label: "Done", onClick: handleSave }}
            cancelAction={{ label: "Cancel", onClick: handleClose }}
            onClose={handleClose}
            overlayClassName="dialog-z-index"
        >
            <CategoyListFormComponentContext.Provider value={{categories: state.categories, selectedCategories: state.dialogOptions.selectedCategories, addCategory, removeCategory}}>
                <TreeView>
                    {hierarchicalList.map(x => <CategoryListItem key={x.categoryID} category={x} level={1} />)}
                </TreeView>
            </CategoyListFormComponentContext.Provider>
        </Dialog>}
    </FormItemWrapper>
}


const CategoryListItem = ({ category, level }: { category: CategoryItem, level: number }) => {

    const hasChildren = () => (category.children?.length ?? 0) > 0;

    const [expand, setExpand] = useState(false);

    const { selectedCategories, addCategory, removeCategory } = useContext(CategoyListFormComponentContext);

    const isSelected = useMemo(() => {
        return selectedCategories?.find(x => x == category.categoryID);
    }, [category, selectedCategories]);

    const toggleCategory = () => {

        if (isSelected) {
            removeCategory && category.categoryID && removeCategory(category.categoryID);
        } else {
            addCategory && category.categoryID && addCategory(category.categoryID);
        }
    }

    return <TreeNode
        name={ category.categoryName + '' }
        nodeIdentifier={category.categoryID + '' }
        isDraggable={true}
        isToggleable={hasChildren()}
        onNodeToggle={(e) => setExpand(e)}
        isSelectable={true}
        isExpanded={expand}
        hasChildren={hasChildren()}
        dropHandler={() => { } }
        renderNode={(x, hovered) => <>
            <TreeNodeLeadingIcon isSelected={x} draggable={false}>
                <Icon name="xp-parent-child-scheme" />
            </TreeNodeLeadingIcon>
            <TreeNodeTitle isSelected={x}>{category.categoryDisplayName}</TreeNodeTitle>
            <TreeNodeTrailingIcon>
                <span style={{ color: isSelected ? "green" : "darkgray", paddingRight: 10 }}>
                    <Icon name={isSelected ? "xp-check-circle" : "xp-circle"} />
                </span>
            </TreeNodeTrailingIcon>
        </>}
        onNodeClick={toggleCategory}
        level={level}>
        {category.children && category.children.map(x => <CategoryListItem key={x.categoryID} category={x} level={level + 1} />)}
    </TreeNode>
}