import Vue from 'vue'
import Vuex from 'vuex'
import ajax from './request';
import restfulClient from './restfulClient'

Vue.use(Vuex)

const state = {
    isSignIned: true,
    groups: {},
    currentService: { 
        name: 'fal',
        description: 'this is a restful service'
 },
    client : restfulClient,
    currentGroup: {},
    theme: '',
    languge: '',
    user: {
        name: 'fal',
        password: ''
    },
    visitor: ajax,
    msgBox: {},
    servicePathMap: {
        refreshAll: '/getall',
        refreshService: '/getservice',
        refreshGroup: '/getgroup',
        removeService: '/removeservice',
        removeGroup: '/removegroup',
        shutdownService: '/shutdownservice',
        shutdownGroup: '/shutdowngroup',
        resumeService: '/resumeservice',
        resumeGroup: '/resumegroup'
    }
}

const mutations = {

    updateCurrentService(state, service) {
        state.currentService = service;
    },
    updateCurrentGroup(state, group) {
        state.currentGroup = group;
    },
    updateSign(stuta) {

    },
    refreshAll(state, groups) {
        state.groups = groups;
    },
    refreshService(state, service) {
        state.groups.get(service.GroupName).set(service.Name, service);
    },
    refreshGroup(state, group) {
        state.groups.set(group.Name, group);
    },
    removeService(state, service) {
        state.groups.get(service.GroupName).delete(service.Name);
    },
    removeGroup(state, group) {
        state.delete(group.Name);
    },
    shutdownService(state, group) {

    },
    resumeService(state, service) {

    },
    shutdownGroup(state, group) {

    },
    resumeGroup(state, group) {

    }

};

const actions = {
    hideFooter(context) {
        context.commit('hide');
    },
    showFooter(context) {
        context.commit('show');
    },
    getNewNum(context, num) {
        context.commit('newNum', num)
    }
};

const store = new Vuex.Store({
    state,
    mutations
});

export default store;
