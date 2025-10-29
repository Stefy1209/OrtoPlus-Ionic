export interface PageMetadata<TEntity> {
    totalCount: number;
    items: TEntity[];
    pageNumber: number;
    pageSize: number;
    totalPages: number;
}