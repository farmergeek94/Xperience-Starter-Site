import { usePageCommand } from '@kentico/xperience-admin-base';
import { Button, ButtonColor, Cols, Column, Divider, DividerOrientation, Headline, HeadlineSize, Input, Paper, Row } from '@kentico/xperience-admin-components';
import React, { useEffect, useReducer, useRef } from 'react'
import './TransformableViewPage.css'
import { TVCategoryListContext, TVCategoryListPageTemplateProperties, TVCategoryListReducer, dialogDefaults, TVDialogAction } from './Components/Methods';
import CategoriesDialog from './Components/GeneralDialog';
import TransformableViewCategoryItem, { ITransformableViewCategoryItem } from '../Shared/TransformableViewCategoryItem';
import TVTreeView from './Components/TVTreeView';
import TVList from './Components/TVList';


export const TransformableViewPageTemplate = ({ categories }: TVCategoryListPageTemplateProperties) => {
    const [state, dispatch] = useReducer(TVCategoryListReducer, {
        categories,
        dialogOptions: dialogDefaults,
    });


    const rowRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (rowRef && rowRef.current) {
            rowRef.current.style.height = "100%";
        }
    }, [rowRef.current])

    const { execute: setCategoryCommand } = usePageCommand<ITransformableViewCategoryItem, ITransformableViewCategoryItem | null>("SetCategory", {
        after: (response) => {
            if (response) {
                dispatch({ type: "categorySet", data: response });
            }
        }
    });

    const { execute: setCategoriesCommand } = usePageCommand<ITransformableViewCategoryItem[], ITransformableViewCategoryItem[] | null>("SetCategories", {
        after: (response) => {
            if (response) {
                dispatch({ type: "categoriesSet", data: response });
            }
        }
    });

    const { execute: deleteCategoryCommand } = usePageCommand<number, number | undefined>("DeleteCategory", {
        after: (response) => {
            if (response) {
                dispatch({ type: "categoryDelete", data: response });
            }
        }
    });

    const setCurrentCategory = (value?: TransformableViewCategoryItem | null) => {
        dispatch({ type: "categorySelect", data: value });
    }

    const setDialog = (data: TVDialogAction) => {
        dispatch({ type: "setDialog", data });
    }

    const setCategory = (item: TransformableViewCategoryItem) => {
        if (!item.transformableViewCategoryID) {
            var siblings = state.categories.filter(x => x.transformableViewCategoryParentID == item.transformableViewCategoryParentID);
            var order = Math.max(...categories.map(x => x.transformableViewCategoryOrder ?? 0), 0);
            item.transformableViewCategoryOrder = order + 1;
        } else {
            dispatch({ type: "categorySet", data: item })
        }
        setCategoryCommand(item);
    }

    const setCategories = (items: ITransformableViewCategoryItem[]) => {
        var sortedItems = items.map((x, i) => { x.transformableViewCategoryOrder = i; return x });
        dispatch({ type: "categoriesSet", data: items })
        setCategoriesCommand(sortedItems);
    }

    const deleteCategory = (id: number) => {
        deleteCategoryCommand(id);
    }

    return <div style={{ display: 'flex', flexDirection: "column", height: "100%" }}>
        <div style={{ paddingBottom: 20 }}><Headline size={HeadlineSize.M}>Transformable Views</Headline></div>
        <div style={{ flex: 1 }}>
            <TVCategoryListContext.Provider value={{ setCategory, setCategories, deleteCategory, setCurrentCategory, dialogOptions: state.dialogOptions, setDialog, categories: state.categories, selectedCategory: state.selectedCategory }}>
                <Row ref={rowRef}>
                    <Column cols={Cols.Col3} className="treeview-wrapper">
                        <Paper fullHeight={true}>
                            <div style={{ padding: 20 }}>
                                <div style={{ paddingBottom: 20 }}><Headline size={HeadlineSize.S}>Categories</Headline></div>
                                <div style={{ paddingBottom: 20 }}><Divider orientation={DividerOrientation.Horizontal} /></div>
                                <TVTreeView />
                            </div>
                        </Paper>
                    </Column>
                    <Column cols={Cols.Col9} className="views-wrapper">
                        <Paper fullHeight={true}>
                            <div style={{ padding: 20 }}>
                                <div style={{ paddingBottom: 20 }}><Headline size={HeadlineSize.S}>Views</Headline></div>
                                <div style={{ paddingBottom: 20 }}><Divider orientation={DividerOrientation.Horizontal} /></div>
                                {state.selectedCategory && state.selectedCategory.transformableViewCategoryID && <TVList categoryID={state.selectedCategory.transformableViewCategoryID} />}
                            </div>
                        </Paper>
                    </Column>
                </Row>
                {state.selectedCategory && <CategoriesDialog />}
            </TVCategoryListContext.Provider>
        </div>
    </div>
}
 


