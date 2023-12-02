export interface ICategoryItem {
    categoryDisplayName: string;
    categoryGuid?: string;
    categoryID?: number;
    categoryLastModified?: Date;
    categoryName?: string;
    categoryParentID?: number | null;
    categoryOrder?: number | null
}
export default interface CategoryItem extends ICategoryItem {
    children?: CategoryItem[]
}