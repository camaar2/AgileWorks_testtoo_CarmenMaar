import { Module } from 'vuex';
import axios from 'axios';
import { RootState } from '@/store/types'; 

interface Inquiry {
    id: number;
    description: string;
    deadline: string; 
}

interface InquiryState {
    inquiries: Inquiry[];
}

const inquiryModule: Module<InquiryState, RootState> = {
    namespaced: true,
    state: {
        inquiries: [],
    },
    mutations: {
        setInquiries(state, inquiries: Inquiry[]) {
            state.inquiries = inquiries;
        },
        addInquiry(state, inquiry: Inquiry) {
            state.inquiries.push(inquiry);
        },
        removeInquiry(state, id: number) {
            state.inquiries = state.inquiries.filter(inquiry => inquiry.id !== id);
        },
    },
    actions: {
        fetchInquiries({ commit }) {
            axios.get('http://localhost:7055/api/Inquiry')
                .then(response => {
                    commit('setInquiries', response.data);
                })
                .catch(error => {
                    console.error('Error fetching inquiries:', error);
                });
        },
        addInquiry({ commit }, newInquiry: Inquiry) {
            axios.post('http://localhost:7055/api/Inquiry', newInquiry)
                .then(response => {
                    commit('addInquiry', response.data);
                })
                .catch(error => {
                    console.error('Error adding inquiry:', error);
                });
        },
        removeInquiry({ commit }, id: number) {
            axios.delete(`http://localhost:7055/api/Inquiry/${id}`)
                .then(() => {
                    commit('removeInquiry', id);
                })
                .catch(error => {
                    console.error('Error deleting inquiry:', error);
                });
        },
    },
};

export default inquiryModule;