import { createContext } from "react";
import CategoryItem, { ICategoryItem } from "../../Shared/CategoryItem";

export interface CategoryListPageTemplateProperties {
    categories: CategoryItem[];
}

export interface CategoryListDialogOptions {
    currentCategory?: CategoryItem | null
    isOpen: boolean
    headline: string
    message: string
    type?: "setCategory" | "deleteCategory"
}

export interface CategoryListPageState extends CategoryListPageTemplateProperties {
    dialogOptions: CategoryListDialogOptions
}

export interface ICategoryListContext {
    setCategory: (value: CategoryItem) => void
    setCategories: (value: CategoryItem[]) => void
    deleteCategory: (value: number) => void
    setCurrentCategory: (value?: CategoryItem | null) => void
    setDialog: (x: CategoryListDialogOptions) => void
    dialogOptions: CategoryListDialogOptions
    categories: CategoryItem[]
}

export type CategoryActions = { type: "categorySet", data: ICategoryItem }
    | { type: "categoriesSet", data: ICategoryItem[] }
    | { type: "categorySelect", data?: ICategoryItem | null }
    | { type: "categoryDelete", data: number }
    | { type: "setDialog", data: CategoryListDialogOptions }

export const CategoryListReducer = (state: CategoryListPageState, action: CategoryActions) => {
    const { type, data } = action;
    const newState = { ...state };
    switch (type) {
        case "categorySet":
            newState.categories = [...newState.categories];
            const objIndex = newState.categories.findIndex((obj => obj.categoryID == data.categoryID));
            if (objIndex > -1) {
                newState.categories[objIndex] = data;
            } else {
                newState.categories.push(data);
            }
            newState.dialogOptions.currentCategory = null;
            break;
        case "categorySelect":
            newState.dialogOptions.currentCategory = data;
            break;
        case "categoryDelete":
            newState.categories = newState.categories.filter(x => x.categoryID != data);
            break;
        case "setDialog":
            newState.dialogOptions = data;
            break;
        case "categoriesSet":
            newState.categories = [...newState.categories];
            for (const cat of data) {
                const objIndex = newState.categories.findIndex((obj => obj.categoryID == cat.categoryID));
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

export const CategoryListContext = createContext<ICategoryListContext>({
    setCurrentCategory: (x?: ICategoryItem | null) => { },
    setCategory: (x: ICategoryItem) => { },
    setCategories: (x: ICategoryItem[]) => { },
    deleteCategory: (x: number) => { },
    setDialog: (x: CategoryListDialogOptions) => { },
    dialogOptions: dialogDefaults,
    categories: []
});