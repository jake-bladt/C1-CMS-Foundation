import expect from 'unittest/helpers/expect.js';
import dataFields, * as actions from 'console/state/reducers/dataFields.js';

describe('Data fields', () => {
	describe('actions', () => {
		it('has action descriptors', () =>
			expect(actions, 'to have property', 'SAVE_STATE')
			.and('to have property', 'UPDATE_VALUE')
		);

		describe('Save state', () => {
			let saveState = actions.saveState;
			it('creates action for saving state', () => {
				let action = saveState('testpage');
				return expect(action, 'to be an action of type', actions.SAVE_STATE)
					.and('to have property', 'pageName', 'testpage');
			});
			// Should also send fields to value end point
		});

		describe('Update field value', () => {
			let updateFieldValue = actions.updateFieldValue;
			it('creates action for updating a field value', () => {
				let action = updateFieldValue('testpage', 'testfield', 'testvalue');
				return expect(action, 'to be an action of type', actions.UPDATE_VALUE)
				.and('to have properties', {
					pageName: 'testpage',
					fieldName: 'testfield',
					newValue: 'testvalue'
				});
			});
		});
	});

	describe('reducer', () => {
		it('outputs an intial state if no action and no previous state', () => {
			let newState = dataFields(undefined, {});
			return expect(newState, 'to equal', { dirtyPages: {} });
		});

		it('outputs the same state object if no action', () => {
			let oldState = { thing: 'do not touch', dirtyPages: {} };
			let newState = dataFields(oldState, {});
			return expect(newState, 'to be', oldState);
		});

		describe('update value', () => {
			let action;
			beforeEach(() => {
				action = {
					type: actions.UPDATE_VALUE,
					pageName: 'testpage',
					fieldName: 'testfield',
					newValue: 'testvalue'
				};
			});

			it('updates a field value', () => {
				let oldState = { thing: 'do not touch', dirtyPages: {} };
				let newState = dataFields(oldState, action);
				return expect(newState, 'not to be', oldState)
				.and('to equal', {
					thing: 'do not touch',
					testfield: 'testvalue',
					dirtyPages: { testpage: ['testfield'] }
				});
			});

			it('only adds page to dirty list if not already on it', () => {
				let oldState = {
					thing: 'do not touch',
					testfield: 'old',
					dirtyPages: { testpage: ['testfield'] }
				};
				let newState = dataFields(oldState, action);
				return expect(newState, 'not to be', oldState)
				.and('to equal', {
					thing: 'do not touch',
					testfield: 'testvalue',
					dirtyPages: { testpage: ['testfield'] }
				});
			});

			it('adds field name to dirty list if not already on it', () => {
				let oldState = {
					thing: 'do not touch',
					testfield: 'old',
					dirtyPages: { testpage: ['otherfield'] }
				};
				let newState = dataFields(oldState, action);
				return expect(newState, 'not to be', oldState)
				.and('to equal', {
					thing: 'do not touch',
					testfield: 'testvalue',
					dirtyPages: { testpage: ['otherfield', 'testfield'] }
				});
			});
		});

		describe('save', () => {
			let action;
			beforeEach(() => {
				action = {
					type: actions.SAVE_STATE,
					pageName: 'testpage'
				};
			});

			it('clears the saved page from the dirty list', () => {
				let oldState = {
					thing: 'do not touch',
					testfield: 'testvalue',
					dirtyPages: { testpage: ['testfield'], otherpage: ['somefield'] }
				};
				let newState = dataFields(oldState, action);
				return expect(newState, 'not to be', oldState)
				.and('to equal', {
					thing: 'do not touch',
					testfield: 'testvalue',
					dirtyPages: { otherpage: ['somefield'] }
				});
			});
		});
	});
});