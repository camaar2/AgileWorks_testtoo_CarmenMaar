<template>
  <div>
    <h1>Carmen Maar TestTöö</h1>
    <form @submit.prevent="submitInquiry">
      <label for="description">Kirjeldus:</label><br>
      <textarea id="description" v-model="inquiry.description" required></textarea><br>
      <label for="deadline">Kuupäev:</label><br>
      <input type="datetime-local" id="deadline" v-model="inquiry.deadline" required><br>
      <button type="submit">Saada</button>
    </form>
    <h2>Aktiivsed ülesanded</h2>
    <ul>
      <li v-for="(inquiry, index) in activeInquiries" :key="index">
        <span :style="{ color: inquiry.isOverdue ? 'red' : 'black' }">{{ inquiry.description }}</span> -
        Kuupäev: {{ inquiry.deadline }}
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      inquiry: {
        description: '',
        deadline: ''
      },
      inquiries: []
    };
  },
  computed: {
    activeInquiries() {
      const now = new Date();
      return this.inquiries.filter(inquiry => {
        return new Date(inquiry.deadline) > now;
      });
    }
  },
  methods: {
    submitInquiry() {
      axios.post('http://localhost:7055/api/Inquiry', this.inquiry)
          .then(response => {
            this.inquiries.push(response.data);
            this.inquiry.description = '';
            this.inquiry.deadline = '';
          })
          .catch(error => {
            console.error('Error submitting inquiry:', error);
          });
    },
    fetchInquiries() {
      axios.get('http://localhost:7055/api/Inquiry')
          .then(response => {
            this.inquiries = response.data;
          })
          .catch(error => {
            console.error('Error fetching inquiries:', error);
          });
    }
  },
  mounted() {
    this.fetchInquiries();
  }
};
</script>

<style scoped>
.read-the-docs {
  color: #888;
}
</style>
