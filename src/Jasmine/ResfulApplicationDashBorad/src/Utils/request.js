import axios from 'axios'
import iview from 'iview'

const ajax = axios.create({
  baseURL: 'http://127.0.0.1:10336/api',
  timeout: 15000
})

/* set request header, should add cookie? */
ajax.interceptors.request.use(config => {
  return config
}, error => {
  Promise.reject(error)
})

/*  intercept response */
ajax.interceptors.response.use(

  response => {
    const result = response.data

    if (result.error !== 0) {
      console.log(result)
      // msgBox.error({ content: result.msg || '请求出现了问题！', duration: 5 });
      return false
    } else {
      return response.data
    }
  },

  err => {
    return Promise.reject(err)
  })

const visitor = {
  getData: (path) => {
    iview.LoadingBar.start();
    ajax.get(path)
      .then(response => {
        iview.LoadingBar.finish();
        return response;
      })
      .catch(_err => {
        // console.log('get error');
        // iview.LoadingBar.error();
      })
  }
}

export default visitor
