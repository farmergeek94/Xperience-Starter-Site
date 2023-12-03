import { createContext } from "react";
import TransformableViewCategoryItem, { ITransformableViewCategoryItem } from "../../Shared/TransformableViewCategoryItem";

export interface TVCategoryListPageTemplateProperties {
    categories: TransformableViewCategoryItem[];
}

export interface TVCategoryListDialogOptions {
    isOpen: boolean
    headline: string
    message: string
    type?: "setCategory" | "deleteCategory"
}

export interface TVCategoryListPageState extends TVCategoryListPageTemplateProperties {
    dialogOptions: TVCategoryListDialogOptions,
    selectedCategory?: TransformableViewCategoryItem | null
}

export interface ITVCategoryListContext {
    setCategory: (value: TransformableViewCategoryItem) => void
    setCategories: (value: TransformableViewCategoryItem[]) => void
    deleteCategory: (value: number) => void
    setCurrentCategory: (value?: TransformableViewCategoryItem | null) => void
    setDialog: (x: TVDialogAction) => void
    dialogOptions: TVCategoryListDialogOptions
    categories: TransformableViewCategoryItem[],
    selectedCategory?: TransformableViewCategoryItem | null
}

export interface TVDialogAction {
    dialogOptions: TVCategoryListDialogOptions,
    selectCategory?: TransformableViewCategoryItem | null
}

export type TVCategoryActions = { type: "categorySet", data: ITransformableViewCategoryItem }
    | { type: "categoriesSet", data: ITransformableViewCategoryItem[] }
    | { type: "categorySelect", data?: ITransformableViewCategoryItem | null }
    | { type: "categoryDelete", data: number }
    | { type: "setDialog", data: TVDialogAction }

export const TVCategoryListReducer = (state: TVCategoryListPageState, action: TVCategoryActions) => {
    const { type, data } = action;
    const newState = { ...state };
    switch (type) {
        case "categorySet":
            newState.categories = [...newState.categories];
            const objIndex = newState.categories.findIndex((obj => obj.transformableViewCategoryID == data.transformableViewCategoryID));
            if (objIndex > -1) {
                newState.categories[objIndex] = data;
            } else {
                newState.categories.push(data);
            }
            newState.selectedCategory = null;
            break;
        case "categorySelect":
            newState.selectedCategory = data;
            break;
        case "categoryDelete":
            newState.categories = newState.categories.filter(x => x.transformableViewCategoryID != data);
            break;
        case "setDialog":
            newState.dialogOptions = data.dialogOptions;
            newState.selectedCategory = data.selectCategory;
            break;
        case "categoriesSet":
            newState.categories = [...newState.categories];
            for (const cat of data) {
                const objIndex = newState.categories.findIndex((obj => obj.transformableViewCategoryID == cat.transformableViewCategoryID));
                if (objIndex > -1) {
                    newState.categories[objIndex] = cat;
                } else {
                    newState.categories.push(cat);
                }
            }
            break;
    }
    return newState;
}

export const dialogDefaults = {
    isOpen: false,
    headline: "",
    message: ""
};

export const TVCategoryListContext = createContext<ITVCategoryListContext>({
    setCurrentCategory: (x?: ITransformableViewCategoryItem | null) => { },
    setCategory: (x: ITransformableViewCategoryItem) => { },
    setCategories: (x: ITransformableViewCategoryItem[]) => { },
    deleteCategory: (x: number) => { },
    setDialog: (x: TVDialogAction) => { },
    dialogOptions: dialogDefaults,
    categories: []
});