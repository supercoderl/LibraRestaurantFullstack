import IHttp, { RequestConfig, HTTP_METHODS } from './interface/iHttp'
import axios from './axiosBuilder'

class HttpClient implements IHttp {
  request(
    url: string,
    params?: object,
    data?: object,
    config = { method: HTTP_METHODS.GET } as RequestConfig
  ): Promise<any> {
    return this._request(url, params, data, config)
  }

  private _request(
    url: string,
    params?: object,
    data?: object,
    config?: RequestConfig
  ): Promise<any> {
    return axios.request({
      url,
      method: config?.method,
      params,
      data,
      headers: config?.headers
    })
  }
}

const httpClient = new HttpClient()

export default httpClient
