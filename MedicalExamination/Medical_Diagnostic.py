#!/usr/bin/env python

import pandas as pd
import numpy as np
from sklearn import preprocessing
from sklearn.model_selection import train_test_split
from sklearn.tree import DecisionTreeClassifier #   Import Decision Tree Classifier
from sklearn import metrics #Import scikit-learn metrics module for accuracy calculation
from sklearn.model_selection import cross_val_score
from sklearn.svm import SVC



Training_Data = pd.read_csv("Training_ArabicData.csv")   # Training DataSet
Testing_Data = pd.read_csv("Testing_ArabicData.csv")     # Testing DataSet

Training_columns = Training_Data.columns
Training_columns = Training_columns[:-1]

xtrain = Training_Data[Training_columns]
ytrain = Training_Data['prognosis']


Testing_columns = Testing_Data.columns
Testing_columns = Testing_columns[:-1]

xtest = Testing_Data[Testing_columns]
ytest = Testing_Data['prognosis']


# Split dataset into training Dataset and test Dataset
# test_size    -> This parameter specifies the size of the testing dataset
# random_state -> Pseudo-random number generator state used for random sampling
# 67% training and 33% test
x_train, x_test, y_train, y_test = train_test_split(xtrain, ytrain, test_size=0.33, random_state=42)

# Create Decision Tree classifer object
model  = DecisionTreeClassifier()

# Train Decision Tree Classifer
clf = model.fit(x_train,y_train)

#print ("Acurracy on the actual test data: ", clf.score(xtest,ytest))  # not correct as the testing (Symptoms)dataset different from trainning (Symptoms)dataset


features = Training_columns

feature_dict = {}
for i,f in enumerate(features):
    feature_dict[f] = i



class Diagnostic_Model:

    def Symptoms_Data(Symptoms_list):
    
          Symptoms = [] 
          for l in range(len(features)): # initialize Symptoms list with zero
                  Symptoms.insert(l,0)
          
          for i in range(len(Symptoms_list)):
                  for sp in range(len(features)):
                      if sp == feature_dict[Symptoms_list[i]] :
                          Symptoms[sp]=1
                          print("symptoms Position = " , sp)
            
          Symptoms = np.array(Symptoms).reshape(1,len(Symptoms))  # convert Symptoms list to array 
          
          print("Result =" , clf.predict(Symptoms))
          
          print(clf.predict_proba(Symptoms)) # Predict class probabilities of the input Symptoms
          
          return (clf.predict(Symptoms))
          
    
  
#-------------------------------------------------------- main Function For Testing ---------------------
#def main():

#    Symptoms_list =[u'كحة',  u'حموضة',u'القيء',u'اّلم المعدة',u'قرحة اللسان']
  #Symptoms_list = [u'مثير للحكة',  u'ثورات_الجلد_العقدي'] #عدوى فطريه
 

#    Diagnostic_Result = Diagnostic_Model.Symptoms_Data(Symptoms_list)

#    print (Diagnostic_Result)
  
#if __name__ == '__main__':
#  main()





