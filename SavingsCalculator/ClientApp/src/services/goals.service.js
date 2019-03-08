import axios from 'axios'

const goalsService = {
  getSavingsGoals () {
    return new Promise((resolve, reject) => {
      axios.get('/api/savings')
        .then(res => {
          resolve(res)
        })
        .catch((reason) => {
          reject(reason)
        })
    })
  }
}

export default goalsService
