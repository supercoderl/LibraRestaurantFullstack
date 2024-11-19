// export default interface Category {
//   class?: string
//   where?: string
//   limit?: number
//   skip?: number
//   order?: string
//   include?: string
//   keys?: string
//   count?: number
// }

export default interface Query {
  class?: string
  where?: any
  skip?: number
  order?: string
  include?: string
  keys?: string
  count?: number
  page?: number
  pageSize?: number
  searchTerm?: string | null
  isAll?: boolean,
  categoryId?: number,
  type?: string,
  itemId?: number
}